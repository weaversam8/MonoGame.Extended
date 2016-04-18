﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGame.Extended.Graphics.Batching
{
    internal class DeferredBatchQueuer<TVertexType> : BatchQueuer<TVertexType>
        where TVertexType : struct, IVertexType
    {
        private readonly List<BatchDrawOperation> _drawOperations = new List<BatchDrawOperation>();
        private readonly BatchDrawOperation _emptyDrawOperation = new BatchDrawOperation((PrimitiveType)(-1), 0, 0, 0, 0, null);
        private BatchDrawOperation _currentOperation;
        private TVertexType[] _vertices;
        private int _usedVertexCount;
        private short[] _indices;
        private int _usedIndexCount;

        internal DeferredBatchQueuer(BatchDrawer<TVertexType> batchDrawer)
            : base(batchDrawer)
        {
            _currentOperation = _emptyDrawOperation;
        }

        private void CreateNewOperationIfNecessary(PrimitiveType primitiveType, int vertexCount, int indexCount, IDrawContext drawContext)
        {
            var wasIndexed = _currentOperation.IndexCount > 0;
            var isIndexed = indexCount > 0;

            var currentOperationPrimitiveType = (PrimitiveType)_currentOperation.PrimitiveType;
            // we do not support merging line strip or triangle strip primitives, i.e., a new draw call is needed for each list or triangle strip
            if (wasIndexed == isIndexed && drawContext.Equals(_currentOperation.DrawContext) && primitiveType == currentOperationPrimitiveType && (primitiveType != PrimitiveType.TriangleStrip || primitiveType != PrimitiveType.LineStrip))
            {
                _currentOperation.VertexCount += vertexCount;
                _currentOperation.IndexCount += indexCount;
                return;
            }

            _currentOperation = new BatchDrawOperation(primitiveType, _usedVertexCount, vertexCount, _usedIndexCount, indexCount, drawContext);
            _drawOperations.Add(_currentOperation);
        }

        internal override void Begin()
        {
            if (_vertices == null)
            {
                _vertices = new TVertexType[BatchDrawer.MaximumVerticesCount];
            }

            if (_indices == null)
            {
                _indices = new short[BatchDrawer.MaximumIndicesCount];
            }
        }

        internal override void End()
        {
            Flush();
        }

        private void Flush()
        {
            if (_usedVertexCount == 0)
            {
                return;
            }

            if (_usedIndexCount == 0)
            {
                BatchDrawer.Select(_vertices);
            }
            else
            {
                BatchDrawer.Select(_vertices, _indices);
            }

            _drawOperations.Sort((a, b) =>
            {
                var firstSortKey = a.DrawContext?.SortKey ?? 0;
                var secondSortKey = b.DrawContext?.SortKey ?? 0;
                return firstSortKey.CompareTo(secondSortKey);
            });

            var currentDrawContext = (IDrawContext)null;

            // ReSharper disable once ForCanBeConvertedToForeach
            for (var index = 0; index < _drawOperations.Count; ++index)
            {
                var operation = _drawOperations[index];
                var operationDrawContext = operation.DrawContext;
                if (currentDrawContext != operationDrawContext)
                {
                    operationDrawContext.End();
                    currentDrawContext = operation.DrawContext;
                    currentDrawContext.Begin();
                }
                var primitiveType = (PrimitiveType)operation.PrimitiveType;
                if (operation.IndexCount == 0)
                {
                    BatchDrawer.Draw(primitiveType, operation.StartVertex, operation.VertexCount, currentDrawContext);
                }
                else
                {
                    BatchDrawer.Draw(primitiveType, operation.StartVertex, operation.VertexCount, operation.StartIndex, operation.IndexCount, currentDrawContext);
                }
            }

            currentDrawContext.End();

            _drawOperations.Clear();
            _currentOperation = _emptyDrawOperation;
            _usedVertexCount = 0;
            _usedIndexCount = 0;
        }

        internal override void Queue(PrimitiveType primitiveType, TVertexType[] vertices, int startVertex, int vertexCount, IDrawContext drawContext = null)
        {
            var remainingVertices = BatchDrawer.MaximumVerticesCount - _usedVertexCount;

            if (vertexCount <= remainingVertices)
            {
                QueueVerticesNoOverflow(primitiveType, vertices, startVertex, vertexCount, drawContext);
            }
            else
            {
                QueueVerticesBufferSplit(primitiveType, vertices, startVertex, vertexCount, drawContext, remainingVertices);
            }
        }

        private void QueueVerticesNoOverflow(PrimitiveType primitiveType, TVertexType[] vertices, int startVertex, int vertexCount, IDrawContext drawContext)
        {
            Array.Copy(vertices, startVertex, _vertices, _usedVertexCount, vertexCount);
            CreateNewOperationIfNecessary(primitiveType, vertexCount, 0, drawContext);
            _usedVertexCount += vertexCount;
        }

        private void QueueVerticesBufferSplit(PrimitiveType primitiveType, TVertexType[] vertices, int startVertex, int vertexCount, IDrawContext drawContext, int verticesSpaceLeft)
        {
            switch (primitiveType)
            {
                case PrimitiveType.LineStrip:
                {
                    if (verticesSpaceLeft < 2)
                    {
                        verticesSpaceLeft = 0;
                        // The single vertex will be added later in the code below
                        ++startVertex;
                        --vertexCount;
                    }
                    break;
                }
                case PrimitiveType.LineList:
                {
                    verticesSpaceLeft -= verticesSpaceLeft % 2;
                    break;
                }
                case PrimitiveType.TriangleStrip:
                {
                    if (verticesSpaceLeft < 4)
                    {
                        verticesSpaceLeft = 0;
                        // The two vertices will be added later in the code below
                        startVertex += 2;
                        vertexCount -= 2;
                    }
                    else
                    {
                        verticesSpaceLeft -= (verticesSpaceLeft - 1) % 2;
                    }
                    break;
                }
                case PrimitiveType.TriangleList:
                {
                    verticesSpaceLeft -= verticesSpaceLeft % 3;
                    break;
                }
            }

            if (verticesSpaceLeft > 0)
            {
                Array.Copy(vertices, startVertex, _vertices, _usedVertexCount, verticesSpaceLeft);
                CreateNewOperationIfNecessary(primitiveType, verticesSpaceLeft, 0, drawContext);
                _usedVertexCount += verticesSpaceLeft;
                vertexCount -= verticesSpaceLeft;
                startVertex += verticesSpaceLeft;
            }

            Flush();

            while (vertexCount >= 0)
            {
                //verticesSpaceLeft = BatchDrawer.MaximumBatchSizeKiloBytes;

                switch (primitiveType)
                {
                    case PrimitiveType.LineStrip:
                    {
                        _vertices[0] = vertices[startVertex - 1];
                        --verticesSpaceLeft;
                        ++_usedVertexCount;
                        break;
                    }
                    case PrimitiveType.LineList:
                    {
                        verticesSpaceLeft -= verticesSpaceLeft % 2;
                        break;
                    }
                    case PrimitiveType.TriangleStrip:
                    {
                        _vertices[0] = vertices[startVertex - 2];
                        _vertices[1] = vertices[startVertex - 1];
                        verticesSpaceLeft -= (verticesSpaceLeft - 1) % 2 + 2;
                        _usedVertexCount += 2;

                        break;
                    }
                    case PrimitiveType.TriangleList:
                    {
                        verticesSpaceLeft -= verticesSpaceLeft % 3;
                        break;
                    }
                }

                var verticesToProcess = Math.Min(verticesSpaceLeft, vertexCount);
                Array.Copy(vertices, startVertex, _vertices, _usedVertexCount, verticesToProcess);
                CreateNewOperationIfNecessary(primitiveType, verticesToProcess, 0, drawContext);
                _usedVertexCount += verticesToProcess;
                vertexCount -= verticesToProcess;

                if (vertexCount == 0)
                {
                    break;
                }

                Flush();
                startVertex += verticesToProcess;
            }
        }

        internal override void Queue(PrimitiveType primitiveType, TVertexType[] vertices, int startVertex, int vertexCount, short[] indices, int startIndex, int indexCount, IDrawContext drawContext = null)
        {
            var remainingVertices = BatchDrawer.MaximumVerticesCount - _usedVertexCount;
            var remainingIndices = BatchDrawer.MaximumIndicesCount - _usedIndexCount;

            var exceedsBatchSpace = (vertexCount > remainingVertices) || (indexCount > remainingIndices);

            if (!exceedsBatchSpace)
            {
                QueueIndexedVerticesNoOverflow(primitiveType, vertices, startVertex, vertexCount, indices, startIndex, indexCount, drawContext);
            }
            else
            {
                QueueIndexedVerticesBufferSplit(primitiveType, vertices, startVertex, vertexCount, indices, startIndex, indexCount, drawContext, remainingVertices, remainingIndices);
            }
        }

        private void QueueIndexedVerticesNoOverflow(PrimitiveType primitiveType, TVertexType[] vertices, int startVertex, int vertexCount, short[] indices, int startIndex, int indexCount, IDrawContext drawContext)
        {
            Array.Copy(vertices, startVertex, _vertices, _usedVertexCount, vertexCount);
            var indexOffset = _currentOperation.VertexCount;
            CreateNewOperationIfNecessary(primitiveType, vertexCount, indexCount, drawContext);
            _usedVertexCount += vertexCount;

            // we can't use Array.Copy to copy the indices because we need to add the offset
            var maxIndexCount = startIndex + indexCount;
            for (var index = startIndex; index < maxIndexCount; ++index)
            {
                _indices[_usedIndexCount++] = (short)(indices[index] + indexOffset);
            }
        }

        private void QueueIndexedVerticesBufferSplit(PrimitiveType type, TVertexType[] vertices, int startVertex, int vertexCount, short[] indices, int startIndex, int indexCount, IDrawContext drawContext, int verticesSpaceLeft, int indicesSpaceLeft)
        {
            switch (type)
            {
                case PrimitiveType.LineStrip:
                {
                    if (verticesSpaceLeft < 2)
                    {
                        verticesSpaceLeft = 0;
                        ++startIndex;
                        --indexCount;
                    }
                    break;
                }
                case PrimitiveType.LineList:
                {
                    verticesSpaceLeft -= verticesSpaceLeft % 2;
                    break;
                }
                case PrimitiveType.TriangleStrip:
                {
                    if (verticesSpaceLeft < 4)
                    {
                        verticesSpaceLeft = 0;
                        startIndex += 2;
                        indexCount -= 2;
                    }
                    else
                    {
                        verticesSpaceLeft -= (verticesSpaceLeft - 1) % 2;
                    }
                    break;
                }
                case PrimitiveType.TriangleList:
                {
                    verticesSpaceLeft -= verticesSpaceLeft % 3;
                    break;
                }
            }

            if (verticesSpaceLeft > 0)
            {
                // vertices and indices are bijective to fill in the remaining space for this batch
                CreateNewOperationIfNecessary(type, verticesSpaceLeft, verticesSpaceLeft, drawContext);
                var maxVertexCount = _usedVertexCount + verticesSpaceLeft;
                for (var vertexIndex = startVertex; vertexIndex < maxVertexCount; ++vertexIndex)
                {
                    _indices[_usedIndexCount++] = (short)vertexIndex;
                    _vertices[_usedVertexCount++] = vertices[indices[startIndex] + startVertex];
                    ++startIndex;
                }
                indexCount -= verticesSpaceLeft;
            }

            Flush();

            while (indexCount >= 0 && vertexCount >= 0)
            {
                //verticesSpaceLeft = BatchDrawer.MaximumBatchSizeKiloBytes;

                switch (type)
                {
                    case PrimitiveType.LineStrip:
                    {
                        _vertices[0] = vertices[indices[startIndex - 1] + startVertex];
                        _indices[0] = 0;

                        --verticesSpaceLeft;
                        ++_usedVertexCount;
                        ++_usedIndexCount;
                        break;
                    }
                    case PrimitiveType.LineList:
                    {
                        verticesSpaceLeft -= verticesSpaceLeft % 2;
                        break;
                    }
                    case PrimitiveType.TriangleStrip:
                    {
                        _vertices[0] = vertices[indices[startIndex - 2] + startVertex];
                        _indices[0] = 0;
                        _vertices[1] = vertices[indices[startIndex - 1] + startVertex];
                        _indices[1] = 1;

                        verticesSpaceLeft -= (verticesSpaceLeft - 1) % 2 + 2;
                        _usedIndexCount += 2;
                        _usedVertexCount += 2;
                        break;
                    }
                    case PrimitiveType.TriangleList:
                    {
                        verticesSpaceLeft -= verticesSpaceLeft % 3;
                        break;
                    }
                }

                var verticesToProcess = 0;
                var indicesToProcess = Math.Min(verticesSpaceLeft, indexCount);

                var maxVertexCount = _usedVertexCount + indicesToProcess;
                for (var vertexIndex = _usedVertexCount; vertexIndex < maxVertexCount; ++vertexIndex)
                {
                    // if vertex is already been seen, use that index
                    // if vertex has not already been seem, create the index and copy the vertex
                    _indices[_usedIndexCount++] = (short)vertexIndex;
                    _vertices[_usedVertexCount++] = vertices[indices[startIndex] + startVertex];
                    ++startIndex;
                }

                CreateNewOperationIfNecessary(type, verticesToProcess, indicesToProcess, drawContext);

                indexCount -= indicesToProcess;
                if (indexCount == 0)
                {
                    break;
                }

                Flush();
            }
        }
    }
}
