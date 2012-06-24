using System;
using MonoTouch.UIKit;
using System.Collections.Generic;
using System.Drawing;
using prode.domain;
using MonoTouch.Dialog;

namespace prode
{
	public class CardPagesDataSource : IPagedViewDataSource {
		private List<Card> _cards;
		private CardsViewController _controller;

		public CardPagesDataSource(CardsViewController controller, List<Card> cards) {
			_controller = controller;
			_cards = cards;
		}	
		
	    public int Pages { get { return _cards.Count ; } }
	
	    public UIViewController GetPage(int i){
	        UIViewController viewController = new UIViewController();
			viewController.View.BackgroundColor = UIColor.FromPatternImage(UIImage.FromFile("Default.png"));

	        viewController.View.AddSubview(new CardView(_cards[i]));
			
			var matchDetailView = new MatchDetailView();
			int verticalOffset = 68;
			
			if (_cards[i].IsEditable()) {
				foreach (var match in _cards[i].Matches) {
					var matches = matchDetailView.BuildForEdit(match, verticalOffset);
					verticalOffset += 28;
					
					viewController.View.AddSubviews(matches);
				}			
				
				var submitCardButton = new GlassButton(new RectangleF (10, 354, 300, 40)) {
	     			NormalColor = UIColor.FromRGBA(222/255f, 222/255f, 225/255f, 0.25f),
	     			HighlightedColor = UIColor.Black
	 			};
				submitCardButton.SetTitle("Guardar Tarjeta", UIControlState.Normal);
				submitCardButton.Font = UIFont.BoldSystemFontOfSize(14);
				submitCardButton.Tapped += delegate {
					//app could have been open for a while, and card might no longer be editable
					if (_cards[i].IsEditable()) {
						AppManager.Current.CardsService.SubmitCard(_cards[i]);
					}
				};
				viewController.View.AddSubview(submitCardButton);
			}
			else {
				foreach (var match in _cards[i].Matches) {
					var matches = matchDetailView.BuildForReadOnly(match, verticalOffset);
					verticalOffset += 28;
					viewController.View.AddSubviews(matches);
				}			

				viewController.View.AddSubview(
					new UILabel{
						Text = string.Format("Fecha cerrada. Obtuviste 15 puntos!"),
						TextAlignment = UITextAlignment.Center,
						Frame = new RectangleF(10,354,300,40),
						TextColor = UIColor.White,
						Font = UIFont.BoldSystemFontOfSize(17),
						BackgroundColor = UIColor.Clear
				});
			}

	        return viewController;
	    }

//	    void _HandleTouchUpInside(object sender, EventArgs e) {
//			AppManager.Current.CardsService.OnGetCardsCompleted += delegate {
//				_controller.ReloadPages();
//			};
//			AppManager.Current.CardsService.GetCardsAsync();
//	    }

    	public void Reload(){
			_cards = AppManager.Current.Repository.Cards;
		}
	}
}

