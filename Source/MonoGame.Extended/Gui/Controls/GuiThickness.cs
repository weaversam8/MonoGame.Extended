namespace MonoGame.Extended.Gui.Controls
{
    public struct GuiThickness
    {
        public GuiThickness(int all)
            : this(all, all, all, all)
        {
        }

        public GuiThickness(int leftRight, int topBottom)
            : this(leftRight, topBottom, leftRight, topBottom)
        {
        }

        public GuiThickness(int left, int top, int right, int bottom)
        {
            Left = left;
            Top = top;
            Right = right;
            Bottom = bottom;
        }

        public int Left { get; }
        public int Top { get; }
        public int Right { get; }
        public int Bottom { get; }
    }
}