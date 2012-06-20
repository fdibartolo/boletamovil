using System;
using System.Collections.Generic;
using prode.domain.constants;

namespace prode.domain
{
	public class CommunityService : BaseAbstractService
	{
		private const string _communityUrl = "https://{0}:{1}@{2}/api/community";
		
		public void GetCommunityStats() {
			if (!AppManager.Current.ConfirmNetworkIsAvailable())
				return;
			
			AppManager.Current.OnNetworkUsageStarted("Comunidad");
			
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
				
				var msg = string.Format("Tourn name: {0} - Group name: {1} - Lider: {2} ({3} pts)", 
								community[0].TournamentName,
								community[0].GroupName,
			                  	community[0].Ranking[0].NickName,
				              	community[0].Ranking[0].Points);
				AppManager.Current.OnNetworkUsageEnded();
				AppManager.Current.ShowMessage("Comunidad", msg);
			}			
			//AppManager.Current.OnNetworkUsageEnded();
		}
	}
}

