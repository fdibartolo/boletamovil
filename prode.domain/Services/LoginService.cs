using System;
using prode.domain.constants;
using System.Collections.Generic;

namespace prode.domain
{
	public delegate void LoginAsyncCompleted(List<string> errors, User user);

	public class LoginService : BaseAbstractService
	{
		private const string _usersUrl = "https://{0}:{1}@{2}/api/users";
		private User _loggedInUser;
		public LoginAsyncCompleted OnLoginCompleted;
		
		public void LoginAsync() {
			if ((string.IsNullOrEmpty(_loginNickName)) || (string.IsNullOrEmpty(_loginPassword)))
				OnLoginCompleted(new List<string> {"There are no credentials set"}, null);
			
			Console.WriteLine("LoginService: Attempting to login async...");
			var url = string.Format(_usersUrl, _loginNickName, _loginPassword, Constants.WEB_SERVER_URL);
			
			var client = new WebClientProxy();
			client.OnHttpGetCompleted += _HttpGetCompleted;
			client.HttpGetAsync(url);
		}
		
		private void _HttpGetCompleted(List<string> errors, string result) {
			if (errors != null)
				OnLoginCompleted(errors, null);
			else {
				_loggedInUser = User.BuildFromJson(result);
				if (!string.IsNullOrEmpty(_loggedInUser.NickName))
					_loggedInUser.Password = _loginPassword;
			
				OnLoginCompleted(null, _loggedInUser);
			}
		}
	}
}
