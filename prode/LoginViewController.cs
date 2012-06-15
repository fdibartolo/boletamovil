using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using prode.domain;

namespace prode
{
	public partial class LoginViewController : UIViewController
	{
		UITabBarController tabBarController;
		ILoginService loginService;

		public LoginViewController () : base ("LoginViewController", null) {
			loginService = new LoginService();
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
			// Perform any additional setup after loading the view, typically from a nib.
		}
		
		public override void ViewDidUnload ()
		{
			base.ViewDidUnload ();
			txtUsername.Dispose();
			txtPassword.Dispose();
			loginService = null;
			
			ReleaseDesignerOutlets ();
		}
		
		public override bool ShouldAutorotateToInterfaceOrientation (UIInterfaceOrientation toInterfaceOrientation)
		{
			// Return true for supported orientations
			return (toInterfaceOrientation != UIInterfaceOrientation.PortraitUpsideDown);
		}
		
		partial void Login (MonoTouch.UIKit.UIButton sender)
		{
			if ((string.IsNullOrEmpty(txtUsername.Text)) || (string.IsNullOrEmpty(txtPassword.Text))) {
				new UIAlertView("Comunidad Prode", "Tanto el usuario como la contraseña deben ser provistos", null, "Ok").Show();
				return;
			}
			
			AppManager.Current.Login(txtUsername.Text, txtPassword.Text);
		}
	}
}

