using System;
using MonoTouch.UIKit;

namespace prode
{
	public class ScreenResolutionMatcher
	{
		public static UIImageView BackgroundImage(){
			return IsTallPhone()
				? new UIImageView(UIImage.FromFile("DefaultTall.png"))
				: new UIImageView(UIImage.FromFile("Default.png"));
		}
		
		public static UIColor BackgroundColorFromImage(){
			return IsTallPhone()
				? UIColor.FromPatternImage(UIImage.FromFile("DefaultTall.png"))
				: UIColor.FromPatternImage(UIImage.FromFile("Default.png"));
		}

		public static System.Drawing.RectangleF FullViewFrame() {
			return IsTallPhone()
				? new System.Drawing.RectangleF(0,0,320,568)
				: new System.Drawing.RectangleF(0,0,320,480);
		}

		public static System.Drawing.SizeF ViewFrame() {
			return IsTallPhone()
				? new System.Drawing.SizeF(320,568)
				: new System.Drawing.SizeF(320,480);
		}

		public static System.Drawing.RectangleF PaginationControlFrame() {
			int offset;
			if (IsTallPhone ())
				offset = IsiOS7 () ? 494 : 484;
			else 
				offset = IsiOS7 () ? 406 : 396;
			return new System.Drawing.RectangleF(0,offset,320,14);
		}

		public static System.Drawing.RectangleF PagedScrollViewFrame() {
			return IsTallPhone()
				? new System.Drawing.RectangleF(0,0,320,514)
				: new System.Drawing.RectangleF(0,0,320,426);
		}
			
		public static int PaginationControlContentHeight() {
			return IsTallPhone() ? 486 : 400;
		}

		public static int PushedToBottomButtonY() {
			if (IsTallPhone ())
				return 400;
			return IsiOS7() ? 364 : 354;
		}

		public static int ContentViewStartingY() {
			return IsiOS7() ? 15 : 0;
		}

		private static bool IsTallPhone(){
			return UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Phone
				&& UIScreen.MainScreen.Bounds.Height * UIScreen.MainScreen.Scale >= 1136;
		}

		public static bool IsiOS7() {
			return UIDevice.CurrentDevice.CheckSystemVersion (7, 0);
		}
	}
}

