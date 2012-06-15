using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;

namespace prode.domain
{
	public interface UIClient {
		bool IsNetworkAvailable();
		void NetworkUsageStarted(bool blockUser);
		void NetworkUsageEnded();
		void ShowMessage(string title, string message);
		void ApplicationStartUpMode(AppMode mode);
	}	
	
	public enum AppMode {
		Login,
		Tabs
	}

	public class AppManager {
		
		private UIClient _uiClient;
		public ILoginService LoginService { get; set; }
		public UserStore UserStore { get; set; }
		public Repository Repository { get; set; }
		
		public static AppManager Current { get {
				return _current;
			}
		}
		static AppManager _current;
		
		public AppManager(UIClient client){
			
			_uiClient = client;
			
			LoginService = new LoginService();
			UserStore = new UserStore();
			Repository = new Repository();

			_current = this;
		}

		public void StartUp(){
			if (!ConfirmNetworkIsAvailable())
				return;
			
			OnNetworkUsageStarted(true);

			Console.WriteLine("Starting up...");
			
			AppMode startUpMode;
			var user = UserStore.ReadUser();
			if ((user != null) && (user.IsValid()) && (LoginService.Login(user.NickName, user.Password))){
				_current.Repository.User = user.Sanitize(); //so we dont keep pwd in memory
				startUpMode = AppMode.Tabs;
				Console.WriteLine("StartedUp in tab mode");
			}
			else
				startUpMode = AppMode.Login;
			
			OnNetworkUsageEnded();
			_uiClient.ApplicationStartUpMode(startUpMode);
		}	
		
		public void Login(string nickname, string password){
			if (!ConfirmNetworkIsAvailable())
				return;

			OnNetworkUsageStarted(true);
			
			AppMode startUpMode;
			if (LoginService.Login(nickname, password)){
				UserStore.SaveUser(LoginService.LoggedInUser);
				Repository.User = LoginService.LoggedInUser.Sanitize(); //so we dont keep pwd in memory
				startUpMode = AppMode.Tabs;
				Console.WriteLine("Redirecting to Tab mode");
			}
			else {
				startUpMode = AppMode.Login;
				Console.WriteLine("Back to login form");
			}
			
			OnNetworkUsageEnded();
			_uiClient.ApplicationStartUpMode(startUpMode);
		}
		
		public void OnNetworkUsageStarted(){
			_uiClient.NetworkUsageStarted(true);
		}
		
		public void OnNetworkUsageStarted(bool blockUser){
			_uiClient.NetworkUsageStarted(blockUser);
		}
		
		public void OnNetworkUsageEnded(){
			_uiClient.NetworkUsageEnded();
		}
		
		public void ShowMessage(string title, string message){
			_uiClient.ShowMessage(title, message);
		}
		
		public bool ConfirmNetworkIsAvailable() {
			var available = _uiClient.IsNetworkAvailable();
			if (!available) 
				ShowMessage("Conexión Internet", "No es posible conectarse a internet, asegurate de estar en una red WiFi, o bien habilitar 3G");
			return available;
		}
	}
}



