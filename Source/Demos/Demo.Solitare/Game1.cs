using System;
using System.Linq;
using Demo.Solitare.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using MonoGame.Extended.Animations;
using MonoGame.Extended.Animations.Tweens;
using MonoGame.Extended.InputListeners;
using MonoGame.Extended.SceneGraphs;
using MonoGame.Extended.Sprites;
using MonoGame.Extended.TextureAtlases;
using MonoGame.Extended.ViewportAdapters;

namespace Demo.Solitare
{
    public class Game1 : Game
    {
        // ReSharper disable once NotAccessedField.Local
        private readonly GraphicsDeviceManager _graphicsDeviceManager;
        private SpriteBatch _spriteBatch;
        private Camera2D _camera;
        private Deck<Card> _deck;
        private Table _table;
        private readonly DragHandler<Card> _dragHandler;
        private readonly Random _random;
        private readonly SceneGraph _sceneGraph;

        public Game1()
        {
            _random = new Random();
            _sceneGraph = new SceneGraph();
            _dragHandler = new DragHandler<Card>();
            _graphicsDeviceManager = new GraphicsDeviceManager(this)
            {
                PreferredBackBufferWidth = 1280,
                PreferredBackBufferHeight = 960
            };

            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            Window.AllowUserResizing = true;
        }

        protected override void Initialize()
        {
            var viewportAdapter = new BoxingViewportAdapter(Window, GraphicsDevice, 
                _graphicsDeviceManager.PreferredBackBufferWidth, _graphicsDeviceManager.PreferredBackBufferHeight);
            _camera = new Camera2D(viewportAdapter);

            var mouseListener = new MouseListenerComponent(this, viewportAdapter);
            mouseListener.MouseClicked += OnMouseClicked;
            mouseListener.MouseDragStart += OnMouseDragStart;
            mouseListener.MouseDrag += OnMouseDrag;
            mouseListener.MouseDragEnd += OnMouseDragEnd;

            Components.Add(new AnimationComponent(this));
            Components.Add(mouseListener);

            base.Initialize();
        }

        private void OnMouseClicked(object sender, MouseEventArgs mouseEventArgs)
        {
            var card = FindCardAt(mouseEventArgs.Position.ToVector2());

            card?.Flip();
        }

        private void OnMouseDragStart(object sender, MouseEventArgs args)
        {
            if (args.Button == MouseButton.Left)
            {
                var position = args.Position.ToVector2();
                var card = FindCardAt(position);

                if (card != null && card.Facing == CardFacing.Up)
                    _dragHandler.StartDrag(args.Position, card);
            }
        }

        private Card FindCardAt(Vector2 position)
        {
            var sceneNode = _sceneGraph.GetSceneNodeAt(position);
            var sprite = sceneNode?.Entities.FirstOrDefault() as Sprite;
            return sprite?.Tag as Card;
        }

        private void OnMouseDrag(object sender, MouseEventArgs args)
        {
            _dragHandler.Drag(args.Position);
        }

        private void OnMouseDragEnd(object sender, MouseEventArgs args)
        {
            var card = _dragHandler.Target;

            if (card != null)
            {
                var foundationSlot = TryDropOnFoundationSlots(args.Position, card);

                card.CreateTweenGroup()
                    .MoveTo(foundationSlot?.Position ?? _dragHandler.TargetStartPosition, 0.2f,
                        EasingFunctions.CubicEaseOut);

                //_table
                //var dropTarget = _allCards.FirstOrDefault(i => i.Contains(args.Position) && i != card);
            }

            _dragHandler.EndDrag();
        }

        private FoundationSlot TryDropOnFoundationSlots(Point position, Card card)
        {
            return _table.FoundationSlots
                .Where(foundationSlot => foundationSlot.BoundingRectangle.Contains(position))
                .FirstOrDefault(foundationSlot => foundationSlot.TryDrop(card));
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            
            var cardAtlas = Content.Load<TextureAtlas>("cards-atlas");

            _table = new Table(cardAtlas[0].Size);
            _deck = NewDeck(cardAtlas);
            _deck.Shuffle(_random);

            var rootNode = _sceneGraph.RootNode;
            rootNode.Entities.Add(_table);
            
            Deal();
        }

        private void Deal()
        {
            var delay = 0.4f;

            for (var k = 0; k < _table.TableauSlots.Length; k++)
            {
                for (var i = k; i < _table.TableauSlots.Length; i++)
                {
                    var tableauSlot = _table.TableauSlots[i];
                    var card = _deck.Draw();

                    var tween = card.CreateTweenChain()
                        .Delay(delay)
                        .MoveTo(tableauSlot + new Vector2(0, k * 40), 0.2f, EasingFunctions.CubicEaseOut);

                    if (i == k)
                        tween.Run(card.Flip);

                    delay += 0.2f;
                }
            }
        }

        private Deck<Card> NewDeck(TextureAtlas cardAtlas)
        {
            var backRegion = cardAtlas["cardBack_blue5"];
            var deck = new Deck<Card>();

            foreach (var suit in Suit.GetAll())
            {
                foreach (var rank in Rank.GetAll())
                {
                    var frontRegion = cardAtlas[$"card{suit}{rank}"];
                    var card = new Card(_sceneGraph.RootNode, rank, suit, frontRegion, backRegion) { Position = _table.DrawSlot };
                    deck.Add(card);
                }
            }

            return deck;
        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            //var deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            var keyboardState = Keyboard.GetState();
            //var mouseState = Mouse.GetState();

            if (keyboardState.IsKeyDown(Keys.Escape))
                Exit();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.DarkGreen);

            _spriteBatch.Begin(blendState: BlendState.AlphaBlend, transformMatrix: _camera.GetViewMatrix());

            //_table.Draw(_spriteBatch);

            //for (var i = _allCards.Count - 1; i >= 0; i--)
            //{
            //    var card = _allCards[i];
            //    card.Draw(_spriteBatch);
            //}

            _sceneGraph.Draw(_spriteBatch);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}