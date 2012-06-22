using System;
using MonoTouch.UIKit;
using System.Drawing;
using prode.domain;
using MonoTouch.Dialog;

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
					Frame = new RectangleF(0,0,320,30),
					TextColor = UIColor.White,
					Font = UIFont.BoldSystemFontOfSize(19),
					BackgroundColor = UIColor.FromRGBA(222/255f, 222/255f, 225/255f, 0.25f)
			});
			
			this.AddSubview(
				new UILabel{
					Text = communityStats.GroupName ?? "Ranking General",
					TextAlignment = UITextAlignment.Center,
					Frame = new RectangleF(0,30,320,30),
					TextColor = UIColor.White,
					Font = UIFont.BoldSystemFontOfSize(15),
					BackgroundColor = UIColor.FromRGBA(222/255f, 222/255f, 225/255f, 0.25f)
			});
			
			this.AddSubview(
				new UILabel{
					Text = "Puntos",
					TextAlignment = UITextAlignment.Center,
					Frame = new RectangleF(260,60,55,20),
					TextColor = UIColor.White,
					Font = UIFont.BoldSystemFontOfSize(13),
					BackgroundColor = UIColor.Clear
			});

			int verticalOffset = 80;
			foreach (var user in communityStats.Ranking) {
				var myRow = _IsMyRow(user.NickName);
				UIView[] ranking = new UIView[] {
				new UILabel {
					Text = user.Position,
					TextAlignment = UITextAlignment.Center,
					Frame = new RectangleF(10, verticalOffset, 35, 24),
					TextColor = UIColor.White,
					Font = myRow ? UIFont.BoldSystemFontOfSize(15): UIFont.SystemFontOfSize(14),
					BackgroundColor = myRow ? UIColor.FromRGBA(222/255f, 222/255f, 225/255f, 0.25f) : UIColor.Clear,
				},
				new UILabel {
					Text = user.NickName,
					TextAlignment = UITextAlignment.Center,
					Frame = new RectangleF(45, verticalOffset, 215, 24),
					TextColor = UIColor.White,
					Font = myRow ? UIFont.BoldSystemFontOfSize(15): UIFont.SystemFontOfSize(14),
					BackgroundColor = myRow ? UIColor.FromRGBA(222/255f, 222/255f, 225/255f, 0.25f) : UIColor.Clear,
				},
				new UILabel {
					Text = user.Points.ToString(),
					TextAlignment = UITextAlignment.Center,
					Frame = new RectangleF(260, verticalOffset, 55, 24),
					TextColor = UIColor.White,
					Font = myRow ? UIFont.BoldSystemFontOfSize(15): UIFont.SystemFontOfSize(14),
					BackgroundColor = myRow ? UIColor.FromRGBA(222/255f, 222/255f, 225/255f, 0.25f) : UIColor.Clear,
				}};

				verticalOffset += 32;
				
				AddSubviews(ranking);
			}
		}
		
		private static bool _IsMyRow(string nickname) {
			return (nickname == AppManager.Current.Repository.User.NickName);
		}
	}
}
