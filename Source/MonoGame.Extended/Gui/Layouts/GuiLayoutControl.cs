using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Gui.Controls;
using MonoGame.Extended.Gui.Drawables;
using MonoGame.Extended.InputListeners;

namespace MonoGame.Extended.Gui.Layouts
{
    //public class GuiLayoutControlTemplate : IGuiControlTemplate
    //{
    //    private readonly GuiLayoutControl _parent;

    //    public GuiLayoutControlTemplate(GuiLayoutControl parent)
    //    {
    //        _parent = parent;
    //    }

    //    public Size CalculateDesiredSize(GuiControl control)
    //    {
    //        return Size.MaxValue;
    //    }

    //    public void Draw(SpriteBatch spriteBatch, GuiControl control)
    //    {
    //        foreach (var child in _parent.Children)
    //            child.Draw(spriteBatch, control.BoundingRectangle);
    //    }
    //}

    //public abstract class GuiLayoutControl : GuiControl
    //{
    //    protected GuiLayoutControl()
    //    {
    //        Children = new List<GuiControl>();
    //    }

    //    public List<GuiControl> Children { get; }

    //    protected override IGuiControlTemplate GetCurrentTemplate()
    //    {
    //        return new GuiLayoutControlTemplate(this);
    //    }

    //    public override void Update(GameTime gameTime)
    //    {
    //        base.Update(gameTime);


    //    }
        




    //    protected int GetHorizontalAlignment(GuiControl control, Rectangle rectangle)
    //    {
    //        switch (control.HorizontalAlignment)
    //        {
    //            case GuiHorizontalAlignment.Stretch:
    //            case GuiHorizontalAlignment.Left:
    //                return rectangle.Left;
    //            case GuiHorizontalAlignment.Right:
    //                return rectangle.Right - control.Width;
    //            case GuiHorizontalAlignment.Center:
    //                return rectangle.Left + rectangle.Width / 2 - control.Width / 2;
    //        }

    //        throw new NotSupportedException($"{control.HorizontalAlignment} is not supported");
    //    }

    //    protected int GetVerticalAlignment(GuiControl control, Rectangle rectangle)
    //    {
    //        switch (control.VerticalAlignment)
    //        {
    //            case GuiVerticalAlignment.Stretch:
    //            case GuiVerticalAlignment.Top:
    //                return rectangle.Top;
    //            case GuiVerticalAlignment.Bottom:
    //                return rectangle.Bottom - control.Height;
    //            case GuiVerticalAlignment.Center:
    //                return rectangle.Top + rectangle.Height / 2 - control.Height / 2;
    //        }

    //        throw new NotSupportedException($"{control.VerticalAlignment} is not supported");
    //    }
    //}
}