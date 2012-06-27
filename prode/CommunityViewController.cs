using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using prode.domain;
using MonoTouch.Dialog;

namespace prode
{
	public partial class CommunityViewController : DialogViewController
	{
		private PagedViewController _pagedViewController;
		
		public CommunityViewController () : base (new RootElement (String.Empty), true)
		{
			Title = NSBundle.MainBundle.LocalizedString ("Comunidad", "Comunidad");
			TabBarItem.Image = UIImage.FromFile("Images/Community.png");
			
			RefreshRequested += _HandleRefreshRequested;
		}

		private void _HandleRefreshRequested (object sender, EventArgs e) {
			if (AppManager.Current.ConfirmNetworkIsAvailable()) {
				AppManager.Current.CommunityService.OnGetCommunityStatsCompleted += delegate {
					ReloadPages();
					this.ReloadComplete();
				};
				AppManager.Current.CommunityService.GetCommunityStatsAsync();
			}
		}
		
		public override void DidReceiveMemoryWarning ()
		{
			// Releases the view if it doesn't have a superview.
			base.DidReceiveMemoryWarning ();
			
			// Release any cached data, images, etc that aren't in use.
		}
		
		public override void ViewDidLoad() {
			base.ViewDidLoad ();
			
			if (_pagedViewController == null) {
				_pagedViewController = new PagedViewController{
	    			//PagedViewDataSource = new CommunityPagesDataSource(this, AppManager.Current.Repository.CommunityStats)
	    			PagedViewDataSource = new CommunityPagesDataSource(AppManager.Current.Repository.CommunityStats)
				};
			}
		}
		
		public override void ViewWillAppear (bool animated) {
			base.ViewWillAppear (animated);
			View.AddSubview(_pagedViewController.View);
			ReloadPages();
		}
		
		public override void ViewDidUnload() {
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

