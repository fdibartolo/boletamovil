using System;

namespace prode.domain
{
	public interface ILoginService
	{
		User LoggedInUser { get; }
		bool Login(string username, string password);
	}
}