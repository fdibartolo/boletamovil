using System;

namespace prode.domain
{
	public interface ILoginService
	{
		bool Login(string username, string password);
	}
}