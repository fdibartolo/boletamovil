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
						
			View.AddSubviews(
				new UILabel{
					Text = "Bienvenido a ComunidadProde!",
					TextAlignment = UITextAlignment.Center,
					Frame = new RectangleF(10,20,300,20),
					Lines = 2,
					TextColor = UIColor.White,
					Font = UIFont.BoldSystemFontOfSize(18),
					BackgroundColor = UIColor.Clear
				},
				new UILabel{
					Text = "Para comenzar a jugar, simplemente andá a www.comunidadprode.com.ar, y elegí el torneo que mas te guste.",
					TextAlignment = UITextAlignment.Center,
					Frame = new RectangleF(10,80,300,60),
					Lines = 3,
					TextColor = UIColor.White,
					Font = UIFont.SystemFontOfSize(16),
					BackgroundColor = UIColor.Clear
				},
				new UILabel{
					Text = "Encotranos en las redes sociales, y compartí con nosotros tus ideas!",
					TextAlignment = UITextAlignment.Center,
					Frame = new RectangleF(10,160,300,40),
					Lines = 2,
					TextColor = UIColor.White,
					Font = UIFont.SystemFontOfSize(16),
					BackgroundColor = UIColor.Clear
				},
				new UILabel{
					Text = "@comunidadprode",
					TextAlignment = UITextAlignment.Center,
					Frame = new RectangleF(10,230,300,20),
					TextColor = UIColor.White,
					Font = UIFont.BoldSystemFontOfSize(18),
					BackgroundColor = UIColor.Clear
				},
				new UILabel{
					Text = "facebook.com/comunidadprode",
					TextAlignment = UITextAlignment.Center,
					Frame = new RectangleF(10,270,300,20),
					TextColor = UIColor.White,
					Font = UIFont.BoldSystemFontOfSize(18),
					BackgroundColor = UIColor.Clear
				},
				new UILabel{
					Text = "comunidadprode@gmail.com",
					TextAlignment = UITextAlignment.Center,
					Frame = new RectangleF(10,310,300,20),
					TextColor = UIColor.White,
					Font = UIFont.BoldSystemFontOfSize(18),
					BackgroundColor = UIColor.Clear
				},
				new UILabel{
					Text = "No te olvides de avisarle a tus amigos!!",
					TextAlignment = UITextAlignment.Center,
					Frame = new RectangleF(10,340,300,40),
					Lines = 2,
					TextColor = UIColor.White,
					Font = UIFont.SystemFontOfSize(16),
					BackgroundColor = UIColor.Clear
				}
			);
			
			var logoutButton = new GlassButton(new RectangleF (10, 410, 300, 40)) {
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

