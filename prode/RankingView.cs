using System;
using MonoTouch.UIKit;
using System.Drawing;
using prode.domain;

namespace prode
{
	public class RankingView : UIView
	{
		public RankingView(Community communityStats)
		{
			this.AddSubview(
				new UILabel{
					Text = communityStats.TournamentName,
					TextAlignment = UITextAlignment.Center,
					Frame = new RectangleF(0,10,320,30),
					TextColor = UIColor.White,
					Font = UIFont.BoldSystemFontOfSize(19),
					BackgroundColor = UIColor.Clear});
			
			this.AddSubview(
				new UILabel{
					Text = communityStats.GroupName ?? "Ranking General",
					TextAlignment = UITextAlignment.Center,
					Frame = new RectangleF(0,40,320,30),
					TextColor = UIColor.White,
					Font = UIFont.BoldSystemFontOfSize(15),
					BackgroundColor = UIColor.Clear});
		}
		
		
	}
}

