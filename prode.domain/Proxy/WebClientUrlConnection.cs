//using System;
//using MonoTouch.Foundation;
//using System.Collections.Generic;
//using MonoTouch.UIKit;
//
//namespace prode.domain
//{
//
//	public class WebClientUrlConnection:NSUrlConnection {
//
//		private static Dictionary<string, WebClientUrlConnection> Connections = new Dictionary<string, WebClientUrlConnection>();
//
//		public static void KillAllConnections() {
//
//			foreach (WebClientUrlConnection c in Connections.Values) {
//				c.Cancel();
//			}
//			Connections.Clear();
//			UIApplication.SharedApplication.NetworkActivityIndicatorVisible = false;
//		}
//
//		protected static void KillConnection(string name) {
//			Connections[name].Cancel();
//			Connections.Remove(name);
//		}
//
//		public static void ConnectionEnded(string name) {
//			Connections.Remove(name);
//		}
//
//		public static bool IsDownloading(string name) {
//			return Connections.ContainsKey(name);	
//		}
//
//		public WebClientUrlConnection(string name, NSUrlRequest request, Action<string> c):base(request, new WebClientUrlConnectionDelegate(name, c), true) {
//			if (Connections.ContainsKey(name)) {
//				KillConnection(name);
//			}
//			Connections.Add(name, this);
//		}
//
//		public WebClientUrlConnection(string name, NSUrlRequest request, Action<string> success, Action failure):base(request, new WebClientUrlConnectionDelegate(name, success, failure), true) {
//			if (Connections.ContainsKey(name)) {
//				KillConnection(name);
//			}
//			Connections.Add(name, this);
//		}
//	}
//
//	public class WebClientUrlConnectionDelegate : NSUrlConnectionDelegate {
//		Action<string> callback;
//		Action _failure;
//		NSMutableData data;
//		string _name;
//
//		public WebClientUrlConnectionDelegate(string name, Action<string> success) {
//			_name = name;
//			callback = success;
//			data = new NSMutableData();
//		}
//
//		public WebClientUrlConnectionDelegate(string name, Action<string> success, Action failure) {
//			_name = name;
//			callback = success;
//			_failure = failure;
//			data = new NSMutableData();
//		}
//
//		public override void ReceivedData (NSUrlConnection connection, NSData d)
//		{
//			data.AppendData(d);
//		}
//
//		public override bool CanAuthenticateAgainstProtectionSpace (NSUrlConnection connection, NSUrlProtectionSpace protectionSpace)
//		{
//			return true;
//		}
//
//		bool showError = true;
//
//		public override void ReceivedAuthenticationChallenge (NSUrlConnection connection, NSUrlAuthenticationChallenge challenge)
//		{
//			if (challenge.PreviousFailureCount>0){
//				showError = false;
//				challenge.Sender.CancelAuthenticationChallenge(challenge);
//				Application.AuthenticationFailure();
//				return;
//			}
//
//			if (challenge.ProtectionSpace.AuthenticationMethod=="NSURLAuthenticationMethodServerTrust")
//				challenge.Sender.UseCredentials(NSUrlCredential.FromTrust(challenge.ProtectionSpace.ServerTrust), challenge);
//
//			if (challenge.ProtectionSpace.AuthenticationMethod=="NSURLAuthenticationMethodDefault" &&
//			    	Application.Account!=null && Application.Account.Login!=null && Application.Account.Password!=null) {
//				challenge.Sender.UseCredentials(NSUrlCredential.FromUserPasswordPersistance(
//				          Application.Account.Login, Application.Account.Password, NSUrlCredentialPersistence.None), challenge);
//
//			}
//		}
//
//		public override void FailedWithError (NSUrlConnection connection, NSError error)
//		{
//			UIApplication.SharedApplication.NetworkActivityIndicatorVisible = false;
//			if (showError)
//				Application.ShowNetworkError(error.LocalizedDescription);
//
//			if (_failure!=null)
//				_failure();
//		}
//
//		public override void FinishedLoading (NSUrlConnection connection)
//		{
//			WebClientUrlConnection.ConnectionEnded(_name);
//			callback(data.ToString());
//		}
//	}
//}