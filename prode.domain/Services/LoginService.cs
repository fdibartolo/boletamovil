using System;
using prode.domain.constants;

namespace prode.domain
{
	public class LoginService : ILoginService
	{
		private const string _usersUrl = "https://{0}:{1}@{2}/api/users";
		
		public LoginService(){}
		
		public bool Login(string username, string password) {
			if ((string.IsNullOrEmpty(username)) || (string.IsNullOrEmpty(password)))
				return false;
			
			var url = string.Format(_usersUrl, username, password, Constants.WEB_SERVER_URL);
			var result = new WebClientProxy().HttpGet(url);

			var user = User.BuildFromJson(result);
			Console.WriteLine(user.FormattedName);
			
			return (result != null);
		}
	}
}
