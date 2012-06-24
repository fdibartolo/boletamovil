using System;
using MonoTouch.UIKit;
using System.Drawing;
using prode.domain;
using MonoTouch.Dialog;

namespace prode
{
	public class MatchDetailView : UIView {
		public UIView[] BuildForReadOnly(Match match, int verticalOffset) {
			var homeRealScore = match.HomeRealScore.HasValue ? match.HomeRealScore.Value.ToString() : "";
			var guestRealScore = match.GuestRealScore.HasValue ? match.GuestRealScore.Value.ToString() : "";
			
			return new UIView[] {
				_BuildLabel(new RectangleF(5, verticalOffset, 100, 24), match.HomeTeam, UIColor.Clear, false),
				_BuildLabel(new RectangleF(110, verticalOffset, 25, 24), 
				            (match.HomeUserScore.HasValue) ? match.HomeUserScore.Value.ToString() : "",
				            _GetColorForUserResult(match.HomeRealScore, match.HomeUserScore), true),
				_BuildLabel(new RectangleF(140, verticalOffset, 40, 24), 
				            string.Format("{0}-{1}", homeRealScore, guestRealScore), 
				            _GetColorForGameResult(match.HomeRealScore, match.HomeUserScore, match.GuestRealScore , match.GuestUserScore), 
				            true),
				_BuildLabel(new RectangleF(185, verticalOffset, 25, 24), 
				            (match.GuestUserScore.HasValue) ? match.GuestUserScore.Value.ToString() : "", 
				            _GetColorForUserResult(match.GuestRealScore, match.GuestUserScore), true),
				_BuildLabel(new RectangleF(215, verticalOffset, 100, 24), match.GuestTeam, UIColor.Clear, false)
			};
		}

		public UIView[] BuildForEdit(Match match, int verticalOffset) {
			var homeRealScore = match.HomeRealScore.HasValue ? match.HomeRealScore.Value.ToString() : "";
			var guestRealScore = match.GuestRealScore.HasValue ? match.GuestRealScore.Value.ToString() : "";
							
			return new UIView[] {
				_BuildLabel(new RectangleF(5, verticalOffset, 100, 24), match.HomeTeam, UIColor.Clear, false),
				_BuildTextField(new RectangleF(110, verticalOffset, 25, 24),
				               (match.HomeUserScore.HasValue) ? match.HomeUserScore.Value.ToString() : ""),
				_BuildLabel(new RectangleF(140, verticalOffset, 40, 24), 
				            string.Format("{0}-{1}", homeRealScore, guestRealScore), 
				            UIColor.FromRGBA(222/255f, 222/255f, 225/255f, 0.25f), true),
				_BuildTextField(new RectangleF(185, verticalOffset, 25, 24),
				               (match.GuestUserScore.HasValue) ? match.GuestUserScore.Value.ToString() : ""),
				_BuildLabel(new RectangleF(215, verticalOffset, 100, 24), match.GuestTeam, UIColor.Clear, false),
//				new FrameView() {
//						Frame = new RectangleF(140, verticalOffset, 40, 24),
//						StrokeColor = UIColor.Red
//					}
			};
		}
		
		private UILabel _BuildLabel(RectangleF frame, string text, UIColor backgroundColor, bool isBold) {
			return new UILabel {
				Text = text,
				TextAlignment = UITextAlignment.Center,
				Frame = frame,
				TextColor = UIColor.White,
				Font = isBold ? UIFont.BoldSystemFontOfSize(14) : UIFont.SystemFontOfSize(14),
				BackgroundColor = backgroundColor, 
			};		
		}

		private UITextField _BuildTextField(RectangleF frame, string text) {
			var field = new UITextField {
				KeyboardType = UIKeyboardType.NumberPad,
				Text = text,
				TextAlignment = UITextAlignment.Center,
				Frame = frame,
				TextColor = UIColor.Black,
				Font = UIFont.BoldSystemFontOfSize(14),
				BackgroundColor = UIColor.White
			};
			field.EditingChanged += (sender, e) => ((UITextField)sender).ResignFirstResponder(); //also save the entry in a hash
			return field;
		}
	
		private UIColor _GetColorForUserResult(int? realScore, int? userScore) {
			if ((!realScore.HasValue) || (!userScore.HasValue))
				return UIColor.FromRGBA(100,0,0,0.5f);
			else if (realScore.Value == userScore.Value)
				return UIColor.FromRGBA(0,100,0,0.3f);
			else
				return UIColor.FromRGBA(100,0,0,0.5f);
		}

		private UIColor _GetColorForGameResult(int? realHomeScore, int? userHomeScore, int? realGuestScore, int? userGuestScore) {
			if ((!realHomeScore.HasValue) || (!userHomeScore.HasValue) 
			    || (!realGuestScore.HasValue) || (!userGuestScore.HasValue))
				return UIColor.FromRGBA(100,0,0,0.5f);
			else if (((realHomeScore.Value > realGuestScore.Value) && (userHomeScore.Value > userGuestScore.Value))
				|| ((realHomeScore.Value == realGuestScore.Value) && (userHomeScore.Value == userGuestScore.Value))
				|| ((realHomeScore.Value < realGuestScore.Value) && (userHomeScore.Value < userGuestScore.Value)))
				return UIColor.FromRGBA(0,100,0,0.3f);
			else
				return UIColor.FromRGBA(100,0,0,0.5f);
		}
	}
}
