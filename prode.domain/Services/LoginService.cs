using System;
using prode.domain.constants;

namespace prode.domain
{
	public delegate void LoginAsyncCompleted(bool hasErrors, User user);

	public class LoginService
	{
		private const string _usersUrl = "https://{0}:{1}@{2}/api/users";
		private User _loggedInUser;
		private string _loginNickName, _loginPassword;
		public LoginAsyncCompleted OnLoginCompleted;
		
		public void SetCredentials (string nickname, string password) {
			_loginNickName = nickname;
			_loginPassword = password;
		}
		
		public void LoginAsync() {
			if ((string.IsNullOrEmpty(_loginNickName)) || (string.IsNullOrEmpty(_loginPassword)))
				throw new ArgumentException("There are not credentials set"); 
					
			Console.WriteLine("LoginService: Attempting to login async...");
			var url = string.Format(_usersUrl, _loginNickName, _loginPassword, Constants.WEB_SERVER_URL);
			var result = new WebClientProxy().HttpGet(url);

			_loggedInUser = User.BuildFromJson(result);
			if (!string.IsNullOrEmpty(_loggedInUser.NickName))
				_loggedInUser.Password = _loginPassword;
			
			OnLoginCompleted(false, _loggedInUser);
		}

	}
}
