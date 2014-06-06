using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using prode.domain;
using prode.domain.constants;
using MonoTouch.Dialog;

namespace prode
{
	public partial class LoginViewController : UIViewController
	{
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
			View.BackgroundColor = ScreenResolutionMatcher.BackgroundColorFromImage();
			
			View.AddSubviews(
				new UILabel{
					Text = "Comunidad Prode",
					TextAlignment = UITextAlignment.Center,
					Frame = new RectangleF(0,0,320,60),
					TextColor = UIColor.White,
					Font = UIFont.BoldSystemFontOfSize(19),
					BackgroundColor = UIColor.FromRGBA(222/255f, 222/255f, 225/255f, 0.25f)
				},
				new UILabel{
					Text = "todavía no sos usuario?",
					TextAlignment = UITextAlignment.Center,
					Frame = new RectangleF(0,235,320,20),
					TextColor = UIColor.White,
					Font = UIFont.BoldSystemFontOfSize(13),
					BackgroundColor = UIColor.Clear
				},
				new UILabel{
					Text = "registrate en www.comunidadprode.com.ar!",
					TextAlignment = UITextAlignment.Center,
					Frame = new RectangleF(0,255,320,20),
					TextColor = UIColor.White,
					Font = UIFont.BoldSystemFontOfSize(13),
					BackgroundColor = UIColor.Clear
				}
			);

			var loginButton = new GlassButton(new RectangleF (50, 185, 220, 40)) {
     			NormalColor = UIColor.FromRGBA(222/255f, 222/255f, 225/255f, 0.25f),
     			HighlightedColor = UIColor.Black
 			};
			loginButton.SetTitle("Ingresar", UIControlState.Normal);
			loginButton.Font = UIFont.BoldSystemFontOfSize(14);
			loginButton.Tapped += _Login;
			View.AddSubview(loginButton);
		}

		private void _Login(GlassButton obj) {
			if ((string.IsNullOrEmpty(txtUsername.Text)) || (string.IsNullOrEmpty(txtPassword.Text))) {
				new UIAlertView(Constants.APP_TITLE, "Tanto el usuario como la contraseña deben ser provistos", null, "Ok").Show();
				return;
			}
			
			AppManager.Current.Login(txtUsername.Text, txtPassword.Text);
		}

		public override void ViewDidUnload () {
			base.ViewDidUnload ();
			txtUsername.Dispose();
			txtPassword.Dispose();
			
			ReleaseDesignerOutlets ();
		}
		
		public override void ViewWillAppear (bool animated) {
			base.ViewWillAppear (animated);
			txtPassword.Text = string.Empty;
		}
		
		public override bool ShouldAutorotateToInterfaceOrientation (UIInterfaceOrientation toInterfaceOrientation)
		{
			// Return true for supported orientations
			//return (toInterfaceOrientation != UIInterfaceOrientation.PortraitUpsideDown);
			return false;
		}

		public override UIStatusBarStyle PreferredStatusBarStyle() {
			return UIStatusBarStyle.LightContent;
		}
	}
}

