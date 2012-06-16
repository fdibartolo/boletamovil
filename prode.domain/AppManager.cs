using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;

namespace prode.domain
{
	public interface UIClient {
		bool IsNetworkAvailable();
		void NetworkUsageStarted(bool blockUser, string title);
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
		public LoginService LoginService { get; set; }
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
			Console.WriteLine("Starting up...");

			if (!ConfirmNetworkIsAvailable())
				return;
			
			OnNetworkUsageStarted("Ingresando");

			var user = UserStore.ReadUser();
			if ((user != null) && (user.IsValid())) {
				LoginService.OnLoginCompleted += _LoginCompleted;
				LoginService.LoginAsync(user.NickName, user.Password);
			}
			else {
				OnNetworkUsageEnded();
				_uiClient.ApplicationStartUpMode(AppMode.Login);
			}
		}	
		
		public void Login(string nickname, string password){
			if (!ConfirmNetworkIsAvailable())
				return;

			OnNetworkUsageStarted("Ingresando");
			LoginService.OnLoginCompleted += _LoginCompleted;
			LoginService.LoginAsync(nickname, password);
		}
		
		private void _LoginCompleted(bool hasErrors, User user) {
			AppMode mode;
			if (hasErrors) {
				//TODO: show error msg
				mode = AppMode.Login;
			}
			else {
				Console.WriteLine("Login success!");
				UserStore.SaveUser(user);
				Repository.User = user.Sanitize(); //so we dont keep pwd in memory
				mode = AppMode.Tabs;
			}
			
			OnNetworkUsageEnded();
			_uiClient.ApplicationStartUpMode(mode);
		}

		public void Logout ()
		{
			var user = new User();
			Repository.User = user;
			UserStore.SaveUser(user);
			_uiClient.ApplicationStartUpMode(AppMode.Login);
		}		
		
		public void OnNetworkUsageStarted(){
			_uiClient.NetworkUsageStarted(true, "Conectando");
		}
		
		public void OnNetworkUsageStarted(string title){
			_uiClient.NetworkUsageStarted(true, title);
		}
		
		public void OnNetworkUsageStarted(bool blockUser, string title){
			_uiClient.NetworkUsageStarted(blockUser, title);
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



