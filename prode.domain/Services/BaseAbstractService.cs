using System;
using System.Collections.Generic;
using prode.domain;
using prode.domain.constants;

namespace prode.domain
{
	public abstract class BaseAbstractService
	{
		protected string _loginNickName, _loginPassword;

		public void SetCredentials (string nickname, string password) {
			_loginNickName = nickname;
			_loginPassword = password;
		}
		
		protected void _HandleError(List<string> errors) {
			if (errors.Contains (Constants.ERROR_INVALID_CREDENTIALS)) {
				AppManager.Current.ShowMessage(Constants.APP_TITLE, Constants.ERROR_INVALID_CREDENTIALS);
				AppManager.Current.ChangeApplicationStartUpMode(AppMode.Login);
			}
			else
				AppManager.Current.ShowMessage(Constants.APP_TITLE, errors [0]);
		}		
	}
}

