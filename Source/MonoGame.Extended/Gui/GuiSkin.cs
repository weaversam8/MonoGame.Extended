using System.Collections.Generic;
using MonoGame.Extended.Gui.Controls;

namespace MonoGame.Extended.Gui
{
    public class GuiSkin
    {
        public GuiSkin()
        {
            _styles = new Dictionary<string, GuiControlStyle>();
        }

        private readonly Dictionary<string, GuiControlStyle> _styles;

        public void AddStyle(string name, GuiControlStyle style)
        {
            _styles.Add(name, style);
        }

        public void AddStyle(GuiControlStyle style)
        {
            var name = style.GetType().Name;
            AddStyle(name, style);
        }

        public T GetStyle<T>(string name) where T : GuiControlStyle
        {
            GuiControlStyle style;

            if (_styles.TryGetValue(name, out style))
                return (T) style;

            throw new KeyNotFoundException($"Style '{name}' not found");
        }

        public T GetStyle<T>() where T : GuiControlStyle
        {
            var name = typeof (T).Name;
            return GetStyle<T>(name);
        }
    }
}