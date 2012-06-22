using System;
using MonoTouch.UIKit;
using System.Collections.Generic;
using System.Drawing;
using prode.domain;
using MonoTouch.CoreGraphics;

namespace prode
{
	public class PagesDataSource : IPagedViewDataSource {
		private List<Community> _stats;
		private CommunityViewController _controller;

		public PagesDataSource(CommunityViewController controller, List<Community> communityStats) {
			_controller = controller;
			_stats = communityStats;
		}	
		
	    public int Pages { get { return _stats.Count ; } }
	
	    public UIViewController GetPage(int i){
			Console.WriteLine("get pages");
	        UIViewController viewController = new UIViewController();
			viewController.View.BackgroundColor = UIColor.FromPatternImage(UIImage.FromFile("Default.png"));

	        viewController.View.AddSubview(new UILabel{
				Text= _stats[i].GroupName ?? "Ranking General",
				TextAlignment = UITextAlignment.Center,
				Frame = new RectangleF(0,10,320,40),
				TextColor = UIColor.White,
				Font = UIFont.BoldSystemFontOfSize(18),
				BackgroundColor = UIColor.Clear
	        });
			
			var refreshButton = UIButton.FromType(UIButtonType.RoundedRect);
			refreshButton.Frame = new RectangleF(260, 10, 40, 40);
			refreshButton.SetTitle("R", UIControlState.Normal);
			refreshButton.TouchUpInside += _HandleTouchUpInside;
			viewController.View.AddSubview(refreshButton);
				
	        return viewController;
	    }

	    void _HandleTouchUpInside(object sender, EventArgs e) {
			AppManager.Current.CommunityService.OnGetCommunityStatsCompleted += delegate {
				_controller.ReloadPages();
			};
			AppManager.Current.CommunityService.GetCommunityStatsAsync();
	    }

    	public void Reload(){
			_stats = AppManager.Current.Repository.CommunityStats;
		}
	}
}

