using System;
using MonoTouch.UIKit;
using System.Collections.Generic;
using System.Drawing;
using prode.domain;

namespace prode
{
	public class CommunityPagesDataSource : IPagedViewDataSource {
		private List<Community> _stats;

		public CommunityPagesDataSource(List<Community> communityStats) {
			_stats = communityStats;
		}	
		
	    public int Pages { get { return _stats.Count ; } }
	
	    public UIViewController GetPage(int i){
	        UIViewController viewController = new UIViewController();
			viewController.View.BackgroundColor = ScreenResolutionMatcher.BackgroundColorFromImage();

			viewController.View.AddSubview(new RankingView(_stats[i]));

//			viewController.View.AddSubview(new UIImageView() {
//				Frame = new RectangleF(3,-5,15,29),
//				Image = UIImage.FromFile("Images/Arrow.png")
//			});
	        return viewController;
	    }

    	public void Reload(){
			_stats = AppManager.Current.Repository.CommunityStats;
		}
	}
}

