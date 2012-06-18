using System;

namespace prode.domain
{
	public abstract class BaseAbstractService
	{
		protected string _loginNickName, _loginPassword;

		public void SetCredentials (string nickname, string password) {
			_loginNickName = nickname;
			_loginPassword = password;
		}
	}
}

