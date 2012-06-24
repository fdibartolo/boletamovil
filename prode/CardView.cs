using System;
using MonoTouch.UIKit;
using System.Drawing;
using prode.domain;
using MonoTouch.Dialog;

namespace prode
{
	public class CardView : UIView
	{
		public CardView(Card card)
		{
			this.AddSubview(
				new UILabel{
					Text = card.TournamentName,
					TextAlignment = UITextAlignment.Center,
					Frame = new RectangleF(0,0,320,30),
					TextColor = UIColor.White,
					Font = UIFont.BoldSystemFontOfSize(19),
					BackgroundColor = UIColor.FromRGBA(222/255f, 222/255f, 225/255f, 0.25f)
			});
			
			this.AddSubview(
				new UILabel{
					Text = card.WeekName,
					TextAlignment = UITextAlignment.Center,
					Frame = new RectangleF(0,30,320,30),
					TextColor = UIColor.White,
					Font = UIFont.BoldSystemFontOfSize(15),
					BackgroundColor = UIColor.FromRGBA(222/255f, 222/255f, 225/255f, 0.25f)
			});
			
			int verticalOffset = 68;
			foreach (var match in card.Matches) {
				var matches = new MatchDetailView().BuildForMatch(match, verticalOffset);
				verticalOffset += 28;
				
				AddSubviews(matches);
			}			
		}
	}
	
	public class MatchDetailView : UIView {
		public UIView[] BuildForMatch(Match match, int verticalOffset) {
			var homeRealScore = match.HomeRealScore.HasValue ? match.HomeRealScore.Value.ToString() : "";
			var guestRealScore = match.GuestRealScore.HasValue ? match.GuestRealScore.Value.ToString() : "";
			return new UIView[] {
				new UILabel {
					Text = match.HomeTeam,
					TextAlignment = UITextAlignment.Center,
					Frame = new RectangleF(5, verticalOffset, 100, 24),
					TextColor = UIColor.White,
					Font = UIFont.SystemFontOfSize(14),
					BackgroundColor = UIColor.Clear
				},
				new UILabel {
					Text = (match.HomeUserScore.HasValue) ? match.HomeUserScore.Value.ToString() : "",
					TextAlignment = UITextAlignment.Center,
					Frame = new RectangleF(110, verticalOffset, 25, 24),
					TextColor = UIColor.Black,
					Font = UIFont.SystemFontOfSize(14),
					BackgroundColor = UIColor.White
				},
				new UILabel {
					Text = string.Format("{0}-{1}", homeRealScore, guestRealScore),
					TextAlignment = UITextAlignment.Center,
					Frame = new RectangleF(140, verticalOffset, 40, 24),
					TextColor = UIColor.White,
					Font = UIFont.SystemFontOfSize(14),
					BackgroundColor = UIColor.FromRGBA(222/255f, 222/255f, 225/255f, 0.25f)
				},
				new UILabel {
					Text = (match.GuestUserScore.HasValue) ? match.GuestUserScore.Value.ToString() : "",
					TextAlignment = UITextAlignment.Center,
					Frame = new RectangleF(185, verticalOffset, 25, 24),
					TextColor = UIColor.Black,
					Font = UIFont.SystemFontOfSize(14),
					BackgroundColor = UIColor.White
				},
				new UILabel {
					Text = match.GuestTeam,
					TextAlignment = UITextAlignment.Center,
					Frame = new RectangleF(215, verticalOffset, 100, 24),
					TextColor = UIColor.White,
					Font = UIFont.SystemFontOfSize(14),
					BackgroundColor = UIColor.Clear
				}
			};
		}
	}
}
