using System;

using prode.domain.constants;

namespace prode.domain
{
	public delegate void GetCardsCompleted();

	public class CardsService : BaseAbstractService
	{
		private const string _getCardsUrl = "https://{0}:{1}@{2}/api/cards";
		public GetCardsCompleted OnGetCardsCompleted;
		
		public void GetCardsAsync() {
			Console.WriteLine("CardsService: Attempting to get cards async...");
			var url = string.Format(_getCardsUrl, _loginNickName, _loginPassword, Constants.WEB_SERVER_URL);
			var result = new WebClientProxy().HttpGet(url);

			
			
			
			OnGetCardsCompleted();
		}

	}
}