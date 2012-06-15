using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
//using System.Net;
//using System.Text;

namespace prode
{
	public partial class CardsViewController : UIViewController
	{
		public CardsViewController () : base ("CardsViewController", null)
		{
			Title = NSBundle.MainBundle.LocalizedString ("Mis Tarjetas", "MisTarjetas");
			TabBarItem.Image = UIImage.FromBundle ("Images/second");
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
			
//			var button = UIButton.FromType(UIButtonType.RoundedRect);
//			button.Frame = new RectangleF(10, 260, 300, 40);
//			button.SetTitle("Tarjetas", UIControlState.Normal);
//			button.TouchUpInside += delegate(object sender, EventArgs e) {
//				WebClient wc = new WebClient();
//            	Uri uri = new Uri("https://fdibartolo:fdibartolo@stormy-autumn-8027.herokuapp.com/api/cards");
//            	byte[] bytes = wc.DownloadData(uri);
//            	string result = Encoding.UTF8.GetString(bytes);
//				new UIAlertView("Title", result, null, "Ok").Show();
//			};
//			View.AddSubview(button);

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

