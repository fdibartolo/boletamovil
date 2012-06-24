using System;
using MonoTouch.UIKit;
using System.Drawing;
using MonoTouch.CoreGraphics;
using MonoTouch.Foundation;
 
namespace prode
{
    public class FrameView : UIView
    {
        CGPath path;
		public UIColor StrokeColor { get; set; }         
             
        public override void Draw (RectangleF rect)
        {
            base.Draw (rect);
             
            using (CGContext gctx = UIGraphics.GetCurrentContext()) {
	            gctx.SetLineWidth(2);
	            UIColor.Clear.SetFill();
	            StrokeColor.SetStroke();
	         
	            path = new CGPath ();
				path.AddRect(rect);
	            path.CloseSubpath();
	             
	            gctx.AddPath(path);    
	            gctx.DrawPath(CGPathDrawingMode.FillStroke);   
			}
        }
	}
}