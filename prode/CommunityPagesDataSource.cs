using System;
using MonoTouch.UIKit;
using System.Collections.Generic;
using System.Drawing;
using prode.domain;

namespace prode
{
	public class CommunityPagesDataSource : IPagedViewDataSource {
		private List<Community> _stats;
		private CommunityViewController _controller;

		public CommunityPagesDataSource(CommunityViewController controller, List<Community> communityStats) {
			_controller = controller;
			_stats = communityStats;
		}	
		
	    public int Pages { get { return _stats.Count ; } }
	
	    public UIViewController GetPage(int i){
	        UIViewController viewController = new UIViewController();
			viewController.View.BackgroundColor = UIColor.FromPatternImage(UIImage.FromFile("Default.png"));

	        viewController.View.AddSubview(new RankingView(_stats[i]));
			
	        return viewController;
	    }

//	    void _HandleTouchUpInside(object sender, EventArgs e) {
//			AppManager.Current.CommunityService.OnGetCommunityStatsCompleted += delegate {
//				_controller.ReloadPages();
//			};
//			AppManager.Current.CommunityService.GetCommunityStatsAsync();
//	    }

    	public void Reload(){
			_stats = AppManager.Current.Repository.CommunityStats;
		}
	}
}

