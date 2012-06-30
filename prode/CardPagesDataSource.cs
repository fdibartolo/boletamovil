using System;
using MonoTouch.UIKit;
using System.Collections.Generic;
using System.Drawing;
using prode.domain;
using MonoTouch.Dialog;
using prode.domain.constants;

namespace prode
{
	public class CardPagesDataSource : IPagedViewDataSource {
		private List<Card> _cards;
		private UIScrollView _scrollView;
		
		public CardPagesDataSource(List<Card> cards) {
			_cards = cards;
		}	
		
	    public int Pages { get { return _cards.Count ; } }
	
	    public UIViewController GetPage(int i){
	        UIViewController viewController = new ScrollableViewController();
			viewController.View.BackgroundColor = UIColor.FromPatternImage(UIImage.FromFile("Default.png"));

			_scrollView = new UIScrollView() {
				Frame = new RectangleF(0,0,320,480),
				ContentSize = new SizeF(320, 480),
                ScrollEnabled = true
			};
	        _scrollView.AddSubview(new CardView(_cards[i]));
			
			var matchDetailView = new MatchDetailView();
			int verticalOffset = 68;
			
			if (_cards[i].IsEditable()) {
				foreach (var match in _cards[i].Matches) {
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
				submitCardButton.Tapped += delegate {
					//app could have been open for a while, and card might no longer be editable
					if (_cards[i].IsEditable())
						AppManager.Current.CardsService.SubmitCard(_cards[i]);
					else 
						new UIAlertView(Constants.APP_TITLE, "La fecha ya ha cerrado.", null, "Ok").Show();
				};
				_scrollView.AddSubview(submitCardButton);
			}
			else {
				foreach (var match in _cards[i].Matches) {
					var matches = matchDetailView.BuildForReadOnly(match, verticalOffset);
					verticalOffset += 28;
					_scrollView.AddSubviews(matches);
				}			

				_scrollView.AddSubview(
					new UILabel{
						Text = string.Format("Fecha cerrada. Obtuviste {0} puntos!", _cards[i].Points),
						TextAlignment = UITextAlignment.Center,
						Frame = new RectangleF(10,354,300,40),
						TextColor = UIColor.White,
						Font = UIFont.BoldSystemFontOfSize(17),
						BackgroundColor = UIColor.Clear
				});
			}

			viewController.View.AddSubview(_scrollView);
	        return viewController;
	    }

    	public void Reload(){
			_cards = AppManager.Current.Repository.Cards;
		}
	}
}

