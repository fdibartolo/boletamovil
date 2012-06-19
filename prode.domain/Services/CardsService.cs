using System;
using prode.domain.constants;
using System.Collections.Generic;

namespace prode.domain
{
	public delegate void GetCardsCompleted(List<string> errors, List<Card> cards);

	public class CardsService : BaseAbstractService
	{
		private const string _getCardsUrl = "https://{0}:{1}@{2}/api/cards";
		public GetCardsCompleted OnGetCardsCompleted;
		
		public void GetCardsAsync() {
			Console.WriteLine("CardsService: Attempting to get cards async...");
			var url = string.Format(_getCardsUrl, _loginNickName, _loginPassword, Constants.WEB_SERVER_URL);
			
			var client = new WebClientProxy();
			client.OnHttpGetCompleted += _HttpGetCompleted;
			client.HttpGetAsync(url);
		}

		private void _HttpGetCompleted(List<string> errors, string result) {
			if (errors != null)
				OnGetCardsCompleted(errors, null);
			else {
				var cards = Card.BuildListOfFromJson(result);
				OnGetCardsCompleted(null, cards);
			}
		}
	}
}