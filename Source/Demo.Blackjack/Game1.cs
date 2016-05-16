using Demo.Solitare.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
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
        private Sprite _sprite;
        private Camera2D _camera;
        private Deck<Card> _deck;
        private Table _table;

        public Game1()
        {
            _graphicsDeviceManager = new GraphicsDeviceManager(this)
            {
                PreferredBackBufferWidth = 1280,
                PreferredBackBufferHeight = 960
            };
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            Window.AllowUserResizing = true;
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            var viewportAdapter = new BoxingViewportAdapter(Window, GraphicsDevice, 
                _graphicsDeviceManager.PreferredBackBufferWidth, _graphicsDeviceManager.PreferredBackBufferHeight);
            _camera = new Camera2D(viewportAdapter);
            
            var cardAtlas = Content.Load<TextureAtlas>("cards-atlas");

            _deck = NewDeck(cardAtlas);

            var logoTexture = Content.Load<Texture2D>("logo-square-128");
            _sprite = new Sprite(logoTexture)
            {
                Position = viewportAdapter.Center.ToVector2()
            };

            _table = new Table(viewportAdapter.VirtualWidth, viewportAdapter.VirtualHeight,
                new Size(cardAtlas[0].Width, cardAtlas[0].Height));
        }

        private static Deck<Card> NewDeck(TextureAtlas cardAtlas)
        {
            var backRegion = cardAtlas["cardBack_blue5"];
            var deck = new Deck<Card>();
            var ranks = new[] {"A", "2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K"};
            var suits = new[] {Suit.Clubs, Suit.Diamonds, Suit.Hearts, Suit.Spades};

            foreach (var suit in suits)
            {
                foreach (var rank in ranks)
                {
                    var frontRegion = cardAtlas[$"card{suit}{rank}"];
                    var card = new Card(new Rank(rank), suit, frontRegion, backRegion);
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
            var deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            var keyboardState = Keyboard.GetState();

            if (keyboardState.IsKeyDown(Keys.Escape))
                Exit();

            _sprite.Rotation += deltaTime;

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.DarkGreen);

            _spriteBatch.Begin(blendState: BlendState.AlphaBlend, transformMatrix: _camera.GetViewMatrix());

            _table.Draw(_spriteBatch);

            for(var i = 0; i < 8; i++)
                _deck[0].Draw(_spriteBatch, _table.DrawSlot);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}