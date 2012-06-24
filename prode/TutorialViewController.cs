using System;
using MonoTouch.UIKit;
using System.Drawing;

namespace prode
{
	public partial class TutorialViewController : UIViewController
	{
		public override void DidReceiveMemoryWarning ()
		{
			// Releases the view if it doesn't have a superview.
			base.DidReceiveMemoryWarning ();
			
			// Release any cached data, images, etc that aren't in use.
		}
		
		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			View.BackgroundColor = UIColor.FromPatternImage(UIImage.FromFile("Default.png"));
						
			View.AddSubview(
				new UILabel{
					Text = "Tutoriallllll",
					TextAlignment = UITextAlignment.Center,
					Frame = new RectangleF(15,40,290,80),
					Lines = 3,
					TextColor = UIColor.White,
					Font = UIFont.SystemFontOfSize(18),
					BackgroundColor = UIColor.Clear
			});
		}
		
		public override void ViewDidUnload ()
		{
			base.ViewDidUnload ();
		}
		
		public override bool ShouldAutorotateToInterfaceOrientation (UIInterfaceOrientation toInterfaceOrientation)
		{
			// Return true for supported orientations
			//return (toInterfaceOrientation != UIInterfaceOrientation.PortraitUpsideDown);
			return false;
		}
	}
}

