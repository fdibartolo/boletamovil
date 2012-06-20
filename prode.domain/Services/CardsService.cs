using System;
using prode.domain.constants;
using System.Collections.Generic;

namespace prode.domain
{
	public class CardsService : BaseAbstractService
	{
		private const string _cardsUrl = "https://{0}:{1}@{2}/api/cards";
		
		public void GetCards() {
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
				var cards = Card.BuildListOfFromJson(result);
				AppManager.Current.Repository.Cards = cards;
				
				var msg = string.Format("Tourn name: {0} - Week name: {1} - UserMatch: {2} ({3}-{4}) {5}", 
				              cards[0].TournamentName,
				              cards[0].WeekName,
			                  cards[0].Matches[1].HomeTeam,
			                  cards[0].Matches[1].HomeUserScore,
			                  cards[0].Matches[1].GuestUserScore,
				              cards[0].Matches[1].GuestTeam);
				AppManager.Current.OnNetworkUsageEnded();
				AppManager.Current.ShowMessage("Tarjetas", msg);
			}
			//AppManager.Current.OnNetworkUsageEnded();
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