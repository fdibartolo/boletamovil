using System;
using MonoTouch.UIKit;
using System.Drawing;
using prode.domain;
using MonoTouch.Dialog;

namespace prode
{
	public class MatchDetailView : UIView {
		public UIView[] BuildForPublished(Match match, int verticalOffset) {
			var homeRealScore = match.HomeRealScore.HasValue ? match.HomeRealScore.Value.ToString() : "";
			var guestRealScore = match.GuestRealScore.HasValue ? match.GuestRealScore.Value.ToString() : "";
			
			UIColor[] homeResultColor = _GetColorForUserResult(match.HomeRealScore, match.HomeUserScore);
			UIColor[] guestResultColor = _GetColorForUserResult(match.GuestRealScore, match.GuestUserScore);
			UIColor[] gameResultColor = _GetColorForGameResult(match.HomeRealScore, match.HomeUserScore, match.GuestRealScore , match.GuestUserScore);
			
			return new UIView[] {
				_BuildLabel(new RectangleF(5, verticalOffset, 100, 24), match.HomeTeam, UIColor.Clear, false),
				new FrameView() { StrokeColor = homeResultColor[1], Frame = new RectangleF(110, verticalOffset, 25, 24)},
				_BuildLabel(new RectangleF(110, verticalOffset, 25, 24), 
				            (match.HomeUserScore.HasValue) ? match.HomeUserScore.Value.ToString() : "",
				            homeResultColor[0], true),
				new FrameView() { StrokeColor = gameResultColor[1], Frame = new RectangleF(140, verticalOffset, 40, 24)},
				_BuildLabel(new RectangleF(140, verticalOffset, 40, 24), 
				            string.Format("{0}-{1}", homeRealScore, guestRealScore), 
				            gameResultColor[0], true),
				new FrameView() { StrokeColor = guestResultColor[1], Frame = new RectangleF(185, verticalOffset, 25, 24)},
				_BuildLabel(new RectangleF(185, verticalOffset, 25, 24), 
				            (match.GuestUserScore.HasValue) ? match.GuestUserScore.Value.ToString() : "", 
				            guestResultColor[0], true),
				_BuildLabel(new RectangleF(215, verticalOffset, 100, 24), match.GuestTeam, UIColor.Clear, false)
			};
		}

		public UIView[] BuildForReadOnly(Match match, int verticalOffset) {
			var homeRealScore = match.HomeRealScore.HasValue ? match.HomeRealScore.Value.ToString() : "";
			var guestRealScore = match.GuestRealScore.HasValue ? match.GuestRealScore.Value.ToString() : "";
							
			return new UIView[] {
				_BuildLabel(new RectangleF(5, verticalOffset, 100, 24), match.HomeTeam, UIColor.Clear, false),
				new FrameView() { StrokeColor = UIColor.Gray, Frame = new RectangleF(110-2, verticalOffset, 25+4, 24)},
				_BuildLabel(new RectangleF(110, verticalOffset+2, 25, 24-4), 
				            (match.HomeUserScore.HasValue) ? match.HomeUserScore.Value.ToString() : "",
				            UIColor.White, true),
				_BuildLabel(new RectangleF(140, verticalOffset, 40, 24), 
				            string.Format("{0}-{1}", homeRealScore, guestRealScore), 
				            UIColor.FromRGBA(222/255f, 222/255f, 225/255f, 0.25f), true),
				new FrameView() { StrokeColor = UIColor.Gray, Frame = new RectangleF(185-2, verticalOffset, 25+4, 24)},
				_BuildLabel(new RectangleF(185, verticalOffset+2, 25, 24-4), 
				            (match.GuestUserScore.HasValue) ? match.GuestUserScore.Value.ToString() : "", 
				            UIColor.White, true),
				_BuildLabel(new RectangleF(215, verticalOffset, 100, 24), match.GuestTeam, UIColor.Clear, false),
			};
		}
		
		public UIView[] BuildForEdit(Match match, int verticalOffset) {
			var homeRealScore = match.HomeRealScore.HasValue ? match.HomeRealScore.Value.ToString() : "";
			var guestRealScore = match.GuestRealScore.HasValue ? match.GuestRealScore.Value.ToString() : "";
							
			return new UIView[] {
				_BuildLabel(new RectangleF(5, verticalOffset, 100, 24), match.HomeTeam, UIColor.Clear, false),
				new FrameView() { StrokeColor = UIColor.Gray, Frame = new RectangleF(110-2, verticalOffset, 25+4, 24)},
				_BuildTextField(new RectangleF(110, verticalOffset+2, 25, 24-4),
				               (match.HomeUserScore.HasValue) ? match.HomeUserScore.Value.ToString() : "",
				               match.MatchId, true),
				_BuildLabel(new RectangleF(140, verticalOffset, 40, 24), 
				            string.Format("{0}-{1}", homeRealScore, guestRealScore), 
				            UIColor.FromRGBA(222/255f, 222/255f, 225/255f, 0.25f), true),
				new FrameView() { StrokeColor = UIColor.Gray, Frame = new RectangleF(185-2, verticalOffset, 25+4, 24)},
				_BuildTextField(new RectangleF(185, verticalOffset+2, 25, 24-4),
				               (match.GuestUserScore.HasValue) ? match.GuestUserScore.Value.ToString() : "",
				               match.MatchId, false),
				_BuildLabel(new RectangleF(215, verticalOffset, 100, 24), match.GuestTeam, UIColor.Clear, false),
			};
		}
		
		private UILabel _BuildLabel(RectangleF frame, string text, UIColor backgroundColor, bool isBold) {
			return new UILabel {
				Text = text,
				TextAlignment = UITextAlignment.Center,
				Frame = frame,
				TextColor = (backgroundColor == UIColor.White) ? UIColor.Gray : UIColor.White,
				Font = isBold ? UIFont.BoldSystemFontOfSize(14) : UIFont.SystemFontOfSize(14),
				BackgroundColor = backgroundColor, 
			};		
		}

		private UITextField _BuildTextField(RectangleF frame, string text, int matchId, bool homeGame) {
			var field = new UITextField {
				KeyboardType = UIKeyboardType.NumberPad,
				Text = text,
				TextAlignment = UITextAlignment.Center,
				Frame = frame,
				TextColor = UIColor.Black,
				Font = UIFont.BoldSystemFontOfSize(14),
				BackgroundColor = UIColor.White,
				Tag = homeGame ? matchId : matchId * -1
			};

			field.EditingChanged += (sender, e) => {
				var result = ((UITextField)sender).Text;

				if (homeGame)
					AppManager.Current.Repository.UpdateHomeResultForMatch(matchId, result);
				else
					AppManager.Current.Repository.UpdateGuestResultForMatch(matchId, result);

				if (result != string.Empty) {
					var nextResponder = ((UITextField)sender).FindNextTextFieldResponder();
					if (nextResponder != null) {
						nextResponder.BecomeFirstResponder();
						((UITextField)nextResponder).ShowDoneButtonOnKeyboard();
					}
				}

			};
			return field;
		}
	
		private UIColor[] _GetColorForUserResult(int? realScore, int? userScore) {
			if ((!realScore.HasValue) || (!userScore.HasValue))
				return new UIColor[] { UIColor.FromRGBA(100,0,0,0.5f), UIColor.Red };
			else if (realScore.Value == userScore.Value)
				return new UIColor[] { UIColor.FromRGBA(0,100,0,0.3f), UIColor.Green };
			else
				return new UIColor[] { UIColor.FromRGBA(100,0,0,0.5f), UIColor.Red };
		}

		private UIColor[] _GetColorForGameResult(int? realHomeScore, int? userHomeScore, int? realGuestScore, int? userGuestScore) {
			if ((!realHomeScore.HasValue) || (!userHomeScore.HasValue) 
			    || (!realGuestScore.HasValue) || (!userGuestScore.HasValue))
				return new UIColor[] { UIColor.FromRGBA(100,0,0,0.5f), UIColor.Red };
			else if (((realHomeScore.Value > realGuestScore.Value) && (userHomeScore.Value > userGuestScore.Value))
				|| ((realHomeScore.Value == realGuestScore.Value) && (userHomeScore.Value == userGuestScore.Value))
				|| ((realHomeScore.Value < realGuestScore.Value) && (userHomeScore.Value < userGuestScore.Value)))
				return new UIColor[] { UIColor.FromRGBA(0,100,0,0.3f), UIColor.Green };
			else
				return new UIColor[] { UIColor.FromRGBA(100,0,0,0.5f), UIColor.Red };
		}
	}
}
