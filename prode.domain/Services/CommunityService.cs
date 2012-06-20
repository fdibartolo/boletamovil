using System;
using System.Collections.Generic;
using prode.domain.constants;

namespace prode.domain
{
	public delegate void GetCommunityAsyncCompleted(List<string> errors, List<Community> community);

	public class CommunityService : BaseAbstractService
	{
		private const string _communityUrl = "https://{0}:{1}@{2}/api/community";
		public GetCommunityAsyncCompleted OnGetCommunityCompleted;
		
		public void GetCommunityAsync() {
			Console.WriteLine("CommunityService: Attempting to get community data async...");
			var url = string.Format(_communityUrl, _loginNickName, _loginPassword, Constants.WEB_SERVER_URL);
			
			var client = new WebClientProxy();
			client.OnHttpGetCompleted += _HttpGetCompleted;
			client.HttpGetAsync(url);
		}
		
		private void _HttpGetCompleted(List<string> errors, string result) {
			if (errors != null)
				OnGetCommunityCompleted(errors, null);
			else {
				var community = Community.BuildListOfFromJson(result);
				OnGetCommunityCompleted(null, community);
			}
		}
	}
}

