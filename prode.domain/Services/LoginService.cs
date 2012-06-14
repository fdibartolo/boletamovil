using System;

namespace prode.domain
{
	public class LoginService : ILoginService
	{
		public LoginService(){}
		
		public bool Login(string username, string password) {
			return true;
		}
	}
}
