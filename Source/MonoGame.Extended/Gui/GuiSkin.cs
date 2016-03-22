using System.Collections.Generic;
using MonoGame.Extended.TextureAtlases;

namespace MonoGame.Extended.Gui
{
    public class GuiSkinElement
    {
        public string Name { get; set; }
    }

    public class GuiSpriteSkinElement
    {
        public string RegionName { get; set; }
    }

    public class GuiNinePatchElement
    {
        public string RegionName { get; set; }
        public int Top { get; set; }
        public int Left { get; set; }
        public int Right { get; set; }
        public int Bottom { get; set; }
    }

    public class GuiSkin
    {
        public GuiSkin()
        {
            Elements = new List<GuiSkinElement>();
        }

        public TextureAtlas TextureAtlas { get; set; }
        public List<GuiSkinElement> Elements { get; }
    }
}