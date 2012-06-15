using System;
using prode.domain.constants;

namespace prode.domain
{
	public class LoginService : ILoginService
	{
		private const string _usersUrl = "https://{0}:{1}@{2}/api/users";
		private User _loggedInUser;
		public User LoggedInUser { get { return _loggedInUser ;}}
		
		public bool Login(string nickname, string password) {
			if ((string.IsNullOrEmpty(nickname)) || (string.IsNullOrEmpty(password)))
				return false;
			
			Console.WriteLine("LoginService: Attempting to login...");
			var url = string.Format(_usersUrl, nickname, password, Constants.WEB_SERVER_URL);
			var result = new WebClientProxy().HttpGet(url);

			_loggedInUser = User.BuildFromJson(result);
			if (!string.IsNullOrEmpty(_loggedInUser.NickName))
				_loggedInUser.Password = password;
			
			return _loggedInUser.IsValid();
		}
	}
}
