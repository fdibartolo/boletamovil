using System;
using System.Drawing;

using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace prode
{
	public partial class LoginViewController : UIViewController
	{
		UITabBarController tabBarController;

		public LoginViewController () : base ("LoginViewController", null) {}
		
		public override void DidReceiveMemoryWarning ()
		{
			// Releases the view if it doesn't have a superview.
			base.DidReceiveMemoryWarning ();
			
			// Release any cached data, images, etc that aren't in use.
		}
		
		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			// Perform any additional setup after loading the view, typically from a nib.
		}
		
		public override void ViewDidUnload ()
		{
			base.ViewDidUnload ();
			txtUsername.Dispose();
			//txtPassword.Dispose();
			
			ReleaseDesignerOutlets ();
		}
		
		public override bool ShouldAutorotateToInterfaceOrientation (UIInterfaceOrientation toInterfaceOrientation)
		{
			// Return true for supported orientations
			return (toInterfaceOrientation != UIInterfaceOrientation.PortraitUpsideDown);
		}
		
		partial void Login (MonoTouch.UIKit.UIButton sender)
		{
			if (txtUsername.Text == "fer") {
				var communityViewController = new CommunityViewController ();
				var cardsViewController = new CardsViewController ();
				var userViewController = new UserViewController();
				tabBarController = new UITabBarController ();
				tabBarController.ViewControllers = new UIViewController [] {
					communityViewController,
					cardsViewController,
					userViewController
				};

				var navigationController = new UINavigationController(tabBarController);
				this.PresentModalViewController(navigationController, true);
			}
			else
				new UIAlertView("ComunidadProde", "El usuario o contrasena son incorrectos", null, "Ok").Show();
		}
	}
}

