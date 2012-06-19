using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
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
	
	public enum AppMode {
		Login,
		Tabs
	}

	public class AppManager {
		
		private UIClient _uiClient;
		public LoginService LoginService { get; set; }
		public CardsService CardsService { get; set; }
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
			CardsService = new CardsService();
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

		void _SetCredentialsForAllServices(string nickname, string password) {
			LoginService.SetCredentials(nickname, password);
			CardsService.SetCredentials(nickname, password);
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
				Repository.User = user.Sanitize(); //so we dont keep pwd in memory
				mode = AppMode.Tabs;
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
		
		public void GetCards() {
			if (!ConfirmNetworkIsAvailable())
				return;

			OnNetworkUsageStarted("Tarjetas");
			CardsService.OnGetCardsCompleted += _GetCardsCompleted;
			CardsService.GetCardsAsync();
		}

		private void _GetCardsCompleted(List<string> errors, List<Card> cards) {
			if (errors != null) {
				if (errors.Contains(Constants.ERROR_INVALID_CREDENTIALS)) {
					ShowMessage(Constants.APP_TITLE, Constants.ERROR_INVALID_CREDENTIALS);
					_uiClient.ApplicationStartUpMode(AppMode.Login);
				}
				else
					ShowMessage(Constants.APP_TITLE, errors[0]);
			}
			else {
				Console.WriteLine("Cards updated!");
				Repository.Cards = cards;
				
				Console.WriteLine("Tourn name: {0}", cards[0].TournamentName);
				Console.WriteLine("Week name: {0}", cards[0].WeekName);
				Console.WriteLine("Match 2: ({0}) {1} ({2}-{3}) {4} ({5})", 
			                  cards[0].Matches[1].HomeUserScore,
			                  cards[0].Matches[1].HomeTeam,
			                  cards[0].Matches[1].HomeRealScore,
			                  cards[0].Matches[1].GuestRealScore,
			                  cards[0].Matches[1].GuestTeam,
			                  cards[0].Matches[1].GuestUserScore);
				
			}
			
			OnNetworkUsageEnded();
		}

		public void SubmitCard() {
			if (!ConfirmNetworkIsAvailable())
				return;

			OnNetworkUsageStarted("Tarjetas");
			CardsService.OnSubmitCardCompleted += _SubmitCardCompleted;

			var sampleData = "{\"card\":{\"week_id\":1,\"matches\":[{\"match_id\":1,\"home_score\":5, \"guest_score\":2},{\"match_id\":2,\"home_score\":2, \"guest_score\":3}]}}";
			CardsService.SubmitCardAsync(sampleData);
		}
		
		private void _SubmitCardCompleted(List<string> errors) {
			if (errors != null) {
				if (errors.Contains(Constants.ERROR_INVALID_CREDENTIALS)) {
					ShowMessage(Constants.APP_TITLE, Constants.ERROR_INVALID_CREDENTIALS);
					_uiClient.ApplicationStartUpMode(AppMode.Login);
				}
				else
					ShowMessage(Constants.APP_TITLE, errors[0]);
			}
			else {
				Console.WriteLine("Card submitted!");
				//TODO: update Repository cards
			}
			
			OnNetworkUsageEnded();
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



