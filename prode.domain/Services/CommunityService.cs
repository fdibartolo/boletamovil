using System;
using System.Collections.Generic;
using prode.domain.constants;

namespace prode.domain
{
	public delegate void GetCommunityStatsCompleted();

	public class CommunityService : BaseAbstractService
	{
		private const string _communityUrl = "https://{0}:{1}@{2}/api/community";
		public GetCommunityStatsCompleted OnGetCommunityStatsCompleted;
		
		public void GetCommunityStats() {
			if (!AppManager.Current.ConfirmNetworkIsAvailable())
				return;
			
			AppManager.Current.OnNetworkUsageStarted("Estadísticas Comunidad");
			
			Console.WriteLine("CommunityService: Attempting to get community data sync...");
			var url = string.Format(_communityUrl, _loginNickName, _loginPassword, Constants.WEB_SERVER_URL);
			
			var client = new WebClientProxy();
			var result = client.HttpGet(url);

			if (!string.IsNullOrEmpty(result))
				AppManager.Current.Repository.CommunityStats = Community.BuildListOfFromJson(result);

			AppManager.Current.OnNetworkUsageEnded();
		}
		
		public void GetCommunityStatsAsync() {
			if (!AppManager.Current.ConfirmNetworkIsAvailable())
				return;
			
			AppManager.Current.OnNetworkUsageStarted("Estadísticas Comunidad");
			
			Console.WriteLine("CommunityService: Attempting to get community data async...");
			var url = string.Format(_communityUrl, _loginNickName, _loginPassword, Constants.WEB_SERVER_URL);
			
			var client = new WebClientProxy();
			client.OnHttpGetCompleted += _HttpGetCompleted;
			client.HttpGetAsync(url);
		}

		private void _HttpGetCompleted(List<string> errors, string result) {
			if (errors != null)
				_HandleError(errors);
			else {
				Console.WriteLine("Community stats updated!");
				var community = Community.BuildListOfFromJson(result);
				AppManager.Current.Repository.CommunityStats = community;
				OnGetCommunityStatsCompleted();
			}			
			AppManager.Current.OnNetworkUsageEnded();
		}
	}
}

