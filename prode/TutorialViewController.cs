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
			View.BackgroundColor = ScreenResolutionMatcher.BackgroundColorFromImage();
						
			View.AddSubviews(
				new UILabel{
					Text = "Bienvenido a ComunidadProde!!",
					TextAlignment = UITextAlignment.Center,
					Frame = new RectangleF(0,0,320,50),
					Lines = 1,
					TextColor = UIColor.White,
					Font = UIFont.BoldSystemFontOfSize(18),
					BackgroundColor = UIColor.FromRGBA(222/255f, 222/255f, 225/255f, 0.25f)
				},
				new UILabel{
				Text = "Estas viendo esta guía porque vemos que aún no has comenzado a jugar.",
					TextAlignment = UITextAlignment.Center,
					Frame = new RectangleF(10,60,300,40),
					Lines = 2,
					TextColor = UIColor.White,
					Font = UIFont.SystemFontOfSize(15),
					BackgroundColor = UIColor.Clear
				},
				new UILabel{
				Text = "Para ello, simplemente entrá en www.comunidadprode.com.ar, y dentro de 'Mis Tarjetas', guardá tu primer tarjeta en el torneo que desees",
					TextAlignment = UITextAlignment.Center,
					Frame = new RectangleF(10,120,300,80),
					Lines = 4,
					TextColor = UIColor.White,
					Font = UIFont.SystemFontOfSize(15),
					BackgroundColor = UIColor.Clear
				},
				new UILabel{
					Text = "Si tenes preguntas, podes visitar la ayuda en la web, o bien contactarnos por las redes sociales!",
					TextAlignment = UITextAlignment.Center,
					Frame = new RectangleF(10,220,300,60),
					Lines = 3,
					TextColor = UIColor.White,
					Font = UIFont.SystemFontOfSize(15),
					BackgroundColor = UIColor.Clear
				},
				new UILabel{
					Text = "@comunidadprode",
					TextAlignment = UITextAlignment.Center,
					Frame = new RectangleF(10,280,300,20),
					TextColor = UIColor.White,
					Font = UIFont.BoldSystemFontOfSize(12),
					BackgroundColor = UIColor.Clear
				},
				new UILabel{
					Text = "facebook.com/comuprode",
					TextAlignment = UITextAlignment.Center,
					Frame = new RectangleF(10,300,300,20),
					TextColor = UIColor.White,
					Font = UIFont.BoldSystemFontOfSize(12),
					BackgroundColor = UIColor.Clear
				},
				new UILabel{
					Text = "contacto@comunidadprode.com.ar",
					TextAlignment = UITextAlignment.Center,
					Frame = new RectangleF(10,320,300,20),
					TextColor = UIColor.White,
					Font = UIFont.BoldSystemFontOfSize(12),
					BackgroundColor = UIColor.Clear
				},
				new UILabel{
					Text = "Gracias por ser parte de la comunidad!!",
					TextAlignment = UITextAlignment.Center,
					Frame = new RectangleF(10,360,300,40),
					Lines = 2,
					TextColor = UIColor.White,
					Font = UIFont.BoldSystemFontOfSize(18),
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

