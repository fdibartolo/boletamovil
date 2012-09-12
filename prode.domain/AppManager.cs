using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using prode.domain.constants;

namespace prode.domain
{
	public class AppManager {
		private UIClient _uiClient;
		public LoginService LoginService { get; set; }
		public CardsService CardsService { get; set; }
		public CommunityService CommunityService { get; set; }
		public UserStore UserStore { get; set; }
		public Repository Repository { get; set; }
		
		static AppManager _current;
		public static AppManager Current { get {
				return _current;
			}
		}
		
		public AppManager(UIClient client){
			_uiClient = client;
			
			LoginService = new LoginService();
			CardsService = new CardsService();
			CommunityService = new CommunityService();
			UserStore = new UserStore();
			Repository = new Repository();

			_current = this;
		}

		public void StartUp(){
			Console.WriteLine("Starting up...");

			var user = UserStore.ReadUser();
			if ((user != null) && (user.IsValid()))
				Login(user.NickName, user.Password);
			else 
				_uiClient.ApplicationStartUpMode(AppMode.Login);
		}	

		public void Login(string nickname, string password){
			if (!ConfirmNetworkIsAvailable())
				return;
			
			_SetCredentialsForAllServices(nickname, password);
			new Thread(new ThreadStart(_LoginAsync)).Start();
		}

		private void _SetCredentialsForAllServices(string nickname, string password) {
			LoginService.SetCredentials(nickname, password);
			CardsService.SetCredentials(nickname, password);
			CommunityService.SetCredentials(nickname, password);
		}
		
		private	void _LoginAsync() {
			OnNetworkUsageStarted("Ingresando");
			LoginService.OnLoginCompleted += _LoginCompleted;
			LoginService.LoginAsync();
		}		

		private void _LoginCompleted(List<string> errors, User user) {
			AppMode mode;
			if (errors != null) {
				ShowMessage(Constants.APP_TITLE, errors[0]);
				mode = AppMode.Login;
			}
			else {
				Console.WriteLine("Login success!");
				UserStore.SaveUser(user);
				Repository.User = user; //.Sanitize(); //so we dont keep pwd in memory
				mode = user.Newbie ? AppMode.Newbie : AppMode.Tabs;
			}
			
			OnNetworkUsageEnded();
			_uiClient.ApplicationStartUpMode(mode);
		}

		public void Logout() {
			var user = new User();
			Repository.User = user;
			UserStore.SaveUser(user);
			_uiClient.ApplicationStartUpMode(AppMode.Login);
		}		
		
		public void ChangeApplicationStartUpMode(AppMode mode){
			_uiClient.ApplicationStartUpMode(mode);
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
