using System;
using prode.domain.constants;

namespace prode.domain
{
	public interface UIClient {
		bool IsNetworkAvailable();
		void NetworkUsageStarted(bool blockUser, string title);
		void NetworkUsageEnded();
		void ShowMessage(string title, string message);
		void ApplicationStartUpMode(AppMode mode);
	}
}

