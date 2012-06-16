using System;
using System.Drawing;
using prode.domain;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace prode
{
	public partial class UserViewController : UIViewController
	{
		public UserViewController () : base ("UserViewController", null)
		{
			Title = NSBundle.MainBundle.LocalizedString ("Mi Cuenta", "MiCuenta");
			TabBarItem.Image = UIImage.FromFile("Images/User.png");
		}
		
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
			
			var button = UIButton.FromType(UIButtonType.RoundedRect);
			button.Frame = new RectangleF(10, 260, 300, 40);
			button.SetTitle("Cerrar Sesion", UIControlState.Normal);
			button.TouchUpInside += delegate(object sender, EventArgs e) {
				AppManager.Current.Logout();
			};
			View.AddSubview(button);
		}
		
		public override void ViewDidUnload ()
		{
			base.ViewDidUnload ();
			
			// Clear any references to subviews of the main view in order to
			// allow the Garbage Collector to collect them sooner.
			//
			// e.g. myOutlet.Dispose (); myOutlet = null;
			
			ReleaseDesignerOutlets ();
		}
		
		public override bool ShouldAutorotateToInterfaceOrientation (UIInterfaceOrientation toInterfaceOrientation)
		{
			// Return true for supported orientations
			return (toInterfaceOrientation != UIInterfaceOrientation.PortraitUpsideDown);
		}
	}
}

