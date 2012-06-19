using System;
using prode.domain.constants;
using System.Collections.Generic;

namespace prode.domain
{
	public delegate void GetCardsCompleted(List<string> errors, List<Card> cards);
	public delegate void SubmitCardCompleted(List<string> errors);

	public class CardsService : BaseAbstractService
	{
		private const string _cardsUrl = "https://{0}:{1}@{2}/api/cards";
		public GetCardsCompleted OnGetCardsCompleted;
		public SubmitCardCompleted OnSubmitCardCompleted;
		
		public void GetCardsAsync() {
			Console.WriteLine("CardsService: Attempting to get cards async...");
			var url = string.Format(_cardsUrl, _loginNickName, _loginPassword, Constants.WEB_SERVER_URL);
			
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
		
		public void SubmitCardAsync(string data) {
			Console.WriteLine("CardsService: Attempting to submit card async...");
			var url = string.Format(_cardsUrl, _loginNickName, _loginPassword, Constants.WEB_SERVER_URL);
			
			var client = new WebClientProxy();
			client.OnHttpPostCompleted += _HttpPostCompleted;
			client.HttpPostAsync(url, data);
		}

		private void _HttpPostCompleted(List<string> errors) {
			OnSubmitCardCompleted(errors);
		}
	}
}