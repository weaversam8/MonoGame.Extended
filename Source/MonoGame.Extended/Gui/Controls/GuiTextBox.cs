using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Gui.Drawables;
using MonoGame.Extended.InputListeners;
using MonoGame.Extended.BitmapFonts;

namespace MonoGame.Extended.Gui.Controls
{
    public class GuiTextBox : GuiContentControl, IGuiTextControl
    {
        private readonly GuiTextBoxStyle _style;
        private const float _cursorBlankRate = 0.5f;
        private float _cursorBlinkDelay = _cursorBlankRate;
        private bool _isCursorVisible = true;

        public GuiTextBox(GuiTextBoxStyle style)
        {
            _style = style;
        }

        protected override IGuiControlTemplate GetCurrentTemplate()
        {
            return _style.BoxTemplate;
        }

        public override IGuiControlTemplate GetCurrentContentTemplate()
        {
            return _style.TextTemplate;
        }

        public string Text { get; set; }

        public override void OnKeyTyped(object sender, KeyboardEventArgs args)
        {
            base.OnKeyTyped(sender, args);

            if (args.Key == Keys.Back && Text.Length > 0)
            {
                Text = Text.Substring(0, Text.Length - 1);
            }
            else
            {
                if (args.Character.HasValue)
                    Text += args.Character;
            }
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            var deltaTime = gameTime.GetElapsedSeconds();

            _cursorBlinkDelay -= deltaTime;

            if (_cursorBlinkDelay <= 0)
            {
                _isCursorVisible = !_isCursorVisible;
                _cursorBlinkDelay = _cursorBlankRate;
            }
        }

        public override void Draw(SpriteBatch spriteBatch, Rectangle rectangle)
        {
            base.Draw(spriteBatch, rectangle);

            if (_isCursorVisible)
            {
                var font = _style.TextTemplate.Font;
                var location = font.MeasureString(Text);
                var position = new Vector2(location.X, 0) + _style.TextTemplate.GetTextLocation(this);
                
                spriteBatch.DrawString(font, "|", position, _style.CursorColor);
            }
        }
    }
}