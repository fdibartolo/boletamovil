using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using prode.domain;

namespace prode
{
	public partial class CommunityViewController : UIViewController
	{
		private PagedViewController _pagedViewController;
		
		public CommunityViewController () : base ("CommunityViewController", null)
		{
			Title = NSBundle.MainBundle.LocalizedString ("Comunidad", "Comunidad");
			TabBarItem.Image = UIImage.FromFile("Images/Community.png");
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
			AppManager.Current.CommunityService.GetCommunityStats(); //sync call
		}
		
		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);
			Console.WriteLine ("community view will appear");
			
			_pagedViewController = new PagedViewController{
    			PagedViewDataSource = new PagesDataSource(this, AppManager.Current.Repository.CommunityStats)
			};
			this.View.AddSubview(_pagedViewController.View);
			ReloadPages();
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
		
		public void ReloadPages() {
			_pagedViewController.ReloadPages();	
		}
	}
}

