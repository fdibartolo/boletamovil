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
			
			DateTime dueDate;
			if ((DateTime.TryParse(_cards[i].WeekDueDate, out dueDate)) && (dueDate > DateTime.Now)) {
				var saveButton = new GlassButton(new RectangleF (10, 354, 300, 40)) {
	     			NormalColor = UIColor.FromRGBA(222/255f, 222/255f, 225/255f, 0.25f),
	     			HighlightedColor = UIColor.Black
	 			};
				saveButton.SetTitle("Guardar Tarjeta", UIControlState.Normal);
				saveButton.Font = UIFont.BoldSystemFontOfSize(14);
				saveButton.Tapped += delegate {
					new UIAlertView("CP", "post card", null,"Ok").Show();
				};
				viewController.View.AddSubview(saveButton);
			}
			else {
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

	    void _HandleTouchUpInside(object sender, EventArgs e) {
			AppManager.Current.CardsService.OnGetCardsCompleted += delegate {
				_controller.ReloadPages();
			};
			AppManager.Current.CardsService.GetCardsAsync();
	    }

    	public void Reload(){
			_cards = AppManager.Current.Repository.Cards;
		}
	}
}

