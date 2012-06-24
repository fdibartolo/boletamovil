using System;
using System.Drawing;
using prode.domain;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using MonoTouch.Dialog;

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
						
			View.AddSubview(
				new UILabel{
					Text = AppManager.Current.Repository.User.FormattedName,
					TextAlignment = UITextAlignment.Right,
					Frame = new RectangleF(10,20,300,40),
					TextColor = UIColor.White,
					Font = UIFont.BoldSystemFontOfSize(24),
					BackgroundColor = UIColor.Clear
			});

			View.AddSubview(
				new UILabel{
					Text = string.Format("({0})", AppManager.Current.Repository.User.NickName),
					TextAlignment = UITextAlignment.Right,
					Frame = new RectangleF(10,50,300,20),
					TextColor = UIColor.White,
					Font = UIFont.BoldSystemFontOfSize(14),
					BackgroundColor = UIColor.Clear
			});

			var logoutButton = new GlassButton(new RectangleF (10, 310, 300, 40)) {
     			NormalColor = UIColor.FromRGBA(222/255f, 222/255f, 225/255f, 0.25f),
     			HighlightedColor = UIColor.Black
 			};
			logoutButton.SetTitle("Cerrar Sesión", UIControlState.Normal);
			logoutButton.Font = UIFont.BoldSystemFontOfSize(14);
			logoutButton.Tapped += delegate {
				AppManager.Current.Logout();
			};
			View.AddSubview(logoutButton);
			
			View.AddSubview(
				new UILabel{
					Text = "Al cerrar la sesión, la próxima vez que abras esta aplicación, deberás volver a ingresar tu usuario y contraseña",
					TextAlignment = UITextAlignment.Center,
					Frame = new RectangleF(15,340,290,80),
					Lines = 3,
					TextColor = UIColor.White,
					Font = UIFont.SystemFontOfSize(12),
					BackgroundColor = UIColor.Clear
			});
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
			//return (toInterfaceOrientation != UIInterfaceOrientation.PortraitUpsideDown);
			return false;
		}
	}
}

