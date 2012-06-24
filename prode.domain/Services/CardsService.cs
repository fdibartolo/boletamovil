using System;
using prode.domain.constants;
using System.Collections.Generic;

namespace prode.domain
{
	public delegate void GetCardsCompleted();

	public class CardsService : BaseAbstractService
	{
		private const string _cardsUrl = "https://{0}:{1}@{2}/api/cards";
		public GetCardsCompleted OnGetCardsCompleted;
		
		public void GetCards() {
			if (!AppManager.Current.ConfirmNetworkIsAvailable())
				return;
			
			AppManager.Current.OnNetworkUsageStarted("Tarjetas");
			
			Console.WriteLine("CardsService: Attempting to get cards sync...");
			var url = string.Format(_cardsUrl, _loginNickName, _loginPassword, Constants.WEB_SERVER_URL);
			
			var client = new WebClientProxy();
			var result = client.HttpGet(url);
			
			if (!string.IsNullOrEmpty(result))
				AppManager.Current.Repository.Cards = Card.BuildListOfFromJson(result);
			
			AppManager.Current.OnNetworkUsageEnded();
		}
		
		public void GetCardsAsync() {
			if (!AppManager.Current.ConfirmNetworkIsAvailable())
				return;

			AppManager.Current.OnNetworkUsageStarted("Tarjetas");

			Console.WriteLine("CardsService: Attempting to get cards async...");
			var url = string.Format(_cardsUrl, _loginNickName, _loginPassword, Constants.WEB_SERVER_URL);
			
			var client = new WebClientProxy();
			client.OnHttpGetCompleted += _HttpGetCompleted;
			client.HttpGetAsync(url);
		}

		private void _HttpGetCompleted(List<string> errors, string result) {
			if (errors != null)
				_HandleError(errors);
			else {
				Console.WriteLine("Cards updated!");
				AppManager.Current.Repository.Cards = Card.BuildListOfFromJson(result);
				OnGetCardsCompleted();
			}
			AppManager.Current.OnNetworkUsageEnded();
		}
		
		public void SubmitCard(string data) {
			if (!AppManager.Current.ConfirmNetworkIsAvailable())
				return;

			AppManager.Current.OnNetworkUsageStarted("Tarjetas");
			
			Console.WriteLine("CardsService: Attempting to submit card async...");
			var url = string.Format(_cardsUrl, _loginNickName, _loginPassword, Constants.WEB_SERVER_URL);
			
			var client = new WebClientProxy();
			client.OnHttpPostCompleted += _HttpPostCompleted;
			client.HttpPostAsync(url, data);
		}

		private void _HttpPostCompleted(List<string> errors) {
			if (errors != null)
				_HandleError(errors);
			else {
				Console.WriteLine("Card submitted!");
				//TODO: update Repository cards
			}
			AppManager.Current.OnNetworkUsageEnded();
		}
	}
}