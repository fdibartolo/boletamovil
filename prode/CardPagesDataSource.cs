using System;
using System.Linq;
using MonoTouch.UIKit;
using System.Collections.Generic;
using System.Drawing;
using prode.domain;
using MonoTouch.Dialog;
using prode.domain.constants;

namespace prode
{
	public class CardPagesDataSource : IPagedViewDataSource {
		private IList<Card> _cards;
		private UIScrollView _scrollView;
		
		public CardPagesDataSource(IList<Card> cards) {
			_cards = cards;
		}	
		
	    public int Pages { get { return _cards.Count ; } }
	
	    public UIViewController GetPage(int i){
			var card = _GetOrderedCardByIndex(i);

	        UIViewController viewController = new ScrollableViewController();
			viewController.View.BackgroundColor = UIColor.FromPatternImage(UIImage.FromFile("Default.png"));

			_scrollView = new UIScrollView() {
				Frame = new RectangleF(0,0,320,480),
				ContentSize = new SizeF(320, 480),
                ScrollEnabled = true
			};
	        _scrollView.AddSubview(new CardView(card));
			
			var matchDetailView = new MatchDetailView();
			int verticalOffset = 68;
			
			if (card.IsEditable()) {
				foreach (var match in card.Matches) {
					var matches = matchDetailView.BuildForEdit(match, verticalOffset);
					verticalOffset += 28;
					
					_scrollView.AddSubviews(matches);
				}			
				
				var submitCardButton = new GlassButton(new RectangleF (10, 354, 300, 40)) {
	     			NormalColor = UIColor.FromRGBA(222/255f, 222/255f, 225/255f, 0.25f),
	     			HighlightedColor = UIColor.Black
	 			};
				submitCardButton.SetTitle("Guardar Tarjeta", UIControlState.Normal);
				submitCardButton.Font = UIFont.BoldSystemFontOfSize(14);
				submitCardButton.Tapped += delegate(GlassButton button) {
					var firstResponder = button.Superview.FindFirstResponder();
					if (firstResponder != null)
						firstResponder.ResignFirstResponder();

					//app could have been open for a while, and card might no longer be editable
					if (card.IsEditable())
						AppManager.Current.CardsService.SubmitCard(card);
					else 
						new UIAlertView(Constants.APP_TITLE, "La fecha ya ha cerrado.", null, "Ok").Show();
				};
				_scrollView.AddSubview(submitCardButton);
			}
			else if (card.IsPublished()) {
				foreach (var match in card.Matches) {
					var matches = matchDetailView.BuildForPublished(match, verticalOffset);
					verticalOffset += 28;
					_scrollView.AddSubviews(matches);
				}			

				_scrollView.AddSubview(
					new UILabel{
						Text = string.Format("Fecha cerrada. Obtuviste {0} puntos!", card.Points),
						TextAlignment = UITextAlignment.Center,
						Frame = new RectangleF(10,354,300,40),
						TextColor = UIColor.White,
						Font = UIFont.BoldSystemFontOfSize(17),
						BackgroundColor = UIColor.Clear
				});
			}
			else {
				foreach (var match in card.Matches) {
					var matches = matchDetailView.BuildForReadOnly(match, verticalOffset);
					verticalOffset += 28;
					_scrollView.AddSubviews(matches);
				}			

				_scrollView.AddSubview(
					new UILabel{
						Text = "Fecha cerrada. Pronto sabrás tus puntos!",
						TextAlignment = UITextAlignment.Center,
						Frame = new RectangleF(10,354,300,40),
						TextColor = UIColor.White,
						Font = UIFont.BoldSystemFontOfSize(15),
						BackgroundColor = UIColor.Clear
				});
			}

			viewController.View.AddSubview(_scrollView);
//			viewController.View.AddSubview(new UIImageView() {
//				Frame = new RectangleF(5,0,16,30),
//				Image = UIImage.FromFile("Images/Arrow.png")
//			});
	        return viewController;
	    }

    	public void Reload(){
			_cards = AppManager.Current.Repository.Cards;
		}

		private Card _GetOrderedCardByIndex(int index) {
			return _cards.OrderBy(c => c.WeekId).ToList()[index];
		}
	}
}

