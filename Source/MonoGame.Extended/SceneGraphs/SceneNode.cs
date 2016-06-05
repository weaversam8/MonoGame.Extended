using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Shapes;

namespace MonoGame.Extended.SceneGraphs
{
    public class SceneNode : IMovable, IRotatable, IScalable
    {
        public SceneNode(string name)
            : this(name, Vector2.Zero, 0, Vector2.One)
        {
        }

        public SceneNode(string name, Vector2 position, float rotation = 0)
            : this(name, position, rotation, Vector2.One)
        {
        }

        public SceneNode(string name, Vector2 position, float rotation, Vector2 scale)
        {
            Name = name;
            Position = position;
            Rotation = rotation;
            Scale = scale;

            Children = new SceneNodeCollection(this);
            Entities = new SceneEntityCollection();
        }

        public SceneNode()
            : this(null, Vector2.Zero, 0, Vector2.One)
        {
        }

        public string Name { get; set; }
        public Vector2 Position { get; set; }
        public float Rotation { get; set; }
        public Vector2 Scale { get; set; }
        public SceneNode Parent { get; internal set; }
        public SceneNodeCollection Children { get; }
        public SceneEntityCollection Entities { get; }
        public object Tag { get; set; }

        private Vector2 _worldPosition;
        public Vector2 WorldPosition
        {
            get
            {
                UpdateWorldTransform();
                return _worldPosition;
            }
        }

        private float _worldRotation;
        public float WorldRotation
        {
            get
            {
                UpdateWorldTransform();
                return _worldRotation;
            }
        }

        private Vector2 _worldScale;
        public Vector2 WorldScale
        {
            get
            {
                UpdateWorldTransform();
                return _worldScale;
            }
        }

        private bool UpdateWorldTransform()
        {
            // ideally if we could efficiently detect that nothing has changed we could skip these calculations
            Vector2 position, scale;
            float rotation;

            if(DecomposeWorldTransform(out position, out rotation, out scale))
            {
                _worldPosition = position;
                _worldRotation = rotation;
                _worldScale = scale;
                return true;
            }

            return false;
        }

        public void Attach(SceneNode sceneNode)
        {
            Children.Add(sceneNode);
        }

        public void Attach(ISceneEntity entity)
        {
            Entities.Add(entity);
        }

        public void Detach(SceneNode sceneNode)
        {
            Children.Remove(sceneNode);
        }

        public void Detach(ISceneEntity entity)
        {
            Entities.Remove(entity);
        }

        public RectangleF GetBoundingRectangle()
        {
            if(!Entities.Any())
                return RectangleF.Empty;

            if (UpdateWorldTransform())
            {
                var rectangles = Entities
                    .Select(e =>
                    {
                        var r = e.GetBoundingRectangle();
                        r.Offset(_worldPosition);
                        return r;
                    })
                    .Concat(Children.Select(i => i.GetBoundingRectangle()))
                    .ToArray();
                var x0 = rectangles.Min(r => r.Left);
                var y0 = rectangles.Min(r => r.Top);
                var x1 = rectangles.Max(r => r.Right);
                var y1 = rectangles.Max(r => r.Bottom);

                return new RectangleF(x0, y0, x1 - x0, y1 - y0);
            }

            return RectangleF.Empty;
        }

        public Matrix GetWorldTransform()
        {
            var localTransform = GetLocalTransform();
            return Parent == null ? localTransform : Matrix.Multiply(localTransform, Parent.GetWorldTransform());
        }

        public bool DecomposeWorldTransform(out Vector2 position, out float rotation, out Vector2 scale)
        {
            return GetWorldTransform()
                .Decompose(out position, out rotation, out scale);
        }

        public Matrix GetLocalTransform()
        {
            var rotationMatrix = Matrix.CreateRotationZ(Rotation);
            var scaleMatrix = Matrix.CreateScale(new Vector3(Scale.X, Scale.Y, 1));
            var translationMatrix = Matrix.CreateTranslation(new Vector3(Position.X, Position.Y, 0));
            var tempMatrix = Matrix.Multiply(scaleMatrix, rotationMatrix);
            return Matrix.Multiply(tempMatrix, translationMatrix);
        }

        public SceneNode FindNodeAt(float x, float y)
        {
            for (var i = Children.Count - 1; i >= 0; i--)
            {
                var childNode = Children[i].FindNodeAt(x, y);

                if (childNode != null)
                    return childNode;
            }

            return GetBoundingRectangle().Contains(x, y) ? this : null;
        }

        public SceneNode FindNodeAt(Vector2 position)
        {
            return FindNodeAt(position.X, position.Y);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Vector2 offsetPosition, offsetScale;
            float offsetRotation;
            var worldTransform = GetWorldTransform();

            if (worldTransform.Decompose(out offsetPosition, out offsetRotation, out offsetScale))
            {
                foreach (var drawable in Entities.OfType<ISceneEntityDrawable>())
                    drawable.Draw(spriteBatch, offsetPosition, offsetRotation, offsetScale);
            }

            foreach (var child in Children)
                child.Draw(spriteBatch);
        }

        public override string ToString()
        {
            return $"name: {Name}, position: {Position}, rotation: {Rotation}, scale: {Scale}";
        }
    }
}
