using System;
using MonoTouch.UIKit;
using System.Drawing;
using MonoTouch.Dialog;
using prode.domain;

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
			
			var logoutButton = new GlassButton(new RectangleF (10, 390, 300, 40)) {
     			NormalColor = UIColor.FromRGBA(222/255f, 222/255f, 225/255f, 0.25f),
     			HighlightedColor = UIColor.Black
 			};
			logoutButton.SetTitle("Cerrar Sesión", UIControlState.Normal);
			logoutButton.Font = UIFont.BoldSystemFontOfSize(14);
			logoutButton.Tapped += delegate {
				AppManager.Current.Logout();
			};
			View.AddSubview(logoutButton);
			
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

