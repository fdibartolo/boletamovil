using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using prode.domain;

namespace prode
{
	public partial class CardsViewController : UIViewController
	{
		public CardsViewController () : base ("CardsViewController", null)
		{
			Title = NSBundle.MainBundle.LocalizedString ("Mis Tarjetas", "MisTarjetas");
			TabBarItem.Image = UIImage.FromFile("Images/Cards.png");
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
			
			var getButton = UIButton.FromType(UIButtonType.RoundedRect);
			getButton.Frame = new RectangleF(10, 200, 300, 40);
			getButton.SetTitle("Get Cards", UIControlState.Normal);
			getButton.TouchUpInside += delegate(object sender, EventArgs e) {
				AppManager.Current.CardsService.GetCards();
			};
			View.AddSubview(getButton);

			var postButton = UIButton.FromType(UIButtonType.RoundedRect);
			postButton.Frame = new RectangleF(10, 260, 300, 40);
			postButton.SetTitle("Submit Sample Card", UIControlState.Normal);
			postButton.TouchUpInside += delegate(object sender, EventArgs e) {
				var sampleData = "{\"card\":{\"week_id\":1,\"matches\":[{\"match_id\":1,\"home_score\":8, \"guest_score\":6},{\"match_id\":2,\"home_score\":7, \"guest_score\":0}]}}";
				AppManager.Current.CardsService.SubmitCard(sampleData);
			};
			View.AddSubview(postButton);
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

