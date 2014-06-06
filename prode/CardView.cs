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
			var initialOffsetY = ScreenResolutionMatcher.ContentViewStartingY();
			this.AddSubview(
				new UILabel{
					Frame = new RectangleF(0,0,320,initialOffsetY),
					BackgroundColor = UIColor.FromRGBA(222/255f, 222/255f, 225/255f, 0.25f)
			});

			this.AddSubview(
				new UILabel{
					Text = card.TournamentName,
					TextAlignment = UITextAlignment.Center,
					Frame = new RectangleF(0,initialOffsetY,320,30),
					TextColor = UIColor.White,
					Font = UIFont.BoldSystemFontOfSize(19),
					BackgroundColor = UIColor.FromRGBA(222/255f, 222/255f, 225/255f, 0.25f)
			});
			
			this.AddSubview(
				new UILabel{
					Text = card.WeekName,
					TextAlignment = UITextAlignment.Center,
					Frame = new RectangleF(0,initialOffsetY+30,320,30),
					TextColor = UIColor.White,
					Font = UIFont.BoldSystemFontOfSize(15),
					BackgroundColor = UIColor.FromRGBA(222/255f, 222/255f, 225/255f, 0.25f)
			});
		}
	}
}
