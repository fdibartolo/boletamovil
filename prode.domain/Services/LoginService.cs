using System;
using prode.domain.constants;

namespace prode.domain
{
	public delegate void LoginAsyncCompleted(bool hasErrors, User user);

	public class LoginService
	{
		private const string _usersUrl = "https://{0}:{1}@{2}/api/users";
		private User _loggedInUser;
		public User LoggedInUser { get { return _loggedInUser ;}}
		public LoginAsyncCompleted OnLoginCompleted;

		public bool Login(string nickname, string password) {
			Console.WriteLine("LoginService: Attempting to login...");
			var url = string.Format(_usersUrl, nickname, password, Constants.WEB_SERVER_URL);
			var result = new WebClientProxy().HttpGet(url);

			_loggedInUser = User.BuildFromJson(result);
			if (!string.IsNullOrEmpty(_loggedInUser.NickName))
				_loggedInUser.Password = password;
			
			return _loggedInUser.IsValid();
		}
		
		public void LoginAsync(string nickname, string password) {
			Console.WriteLine("LoginService: Attempting to login async...");
			var url = string.Format(_usersUrl, nickname, password, Constants.WEB_SERVER_URL);
			var result = new WebClientProxy().HttpGet(url);

			_loggedInUser = User.BuildFromJson(result);
			if (!string.IsNullOrEmpty(_loggedInUser.NickName))
				_loggedInUser.Password = password;
			
			OnLoginCompleted(false, _loggedInUser);
		}

	}
}
