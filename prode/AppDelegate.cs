using System;
using System.Collections.Generic;
using System.Linq;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using prode.domain;
using System.Threading;
using prode.domain.constants;

namespace prode
{
	// The UIApplicationDelegate for the application. This class is responsible for launching the 
	// User Interface of the application, as well as listening (and optionally responding) to 
	// application events from iOS.
	[Register ("AppDelegate")]
	public partial class AppDelegate : UIApplicationDelegate, UIClient
	{
		UIWindow window;
		private LoadingHUDView _loadingView;
		private UIViewController _loginViewController;
		private UIViewController _communityViewController;
		private UIViewController _cardsViewController;
		private UIViewController _userViewController;
		private UIViewController _tutorialViewController;

		//
		// This method is invoked when the application has loaded and is ready to run. In this 
		// method you should instantiate the window, load the UI into it and then make the window
		// visible.
		//
		// You have 17 seconds to return from this method, or iOS will terminate your application.
		//
		public override bool FinishedLaunching (UIApplication app, NSDictionary options)
		{
			new AppManager(this);
			_InitializeControllers();
			
			window = new UIWindow (UIScreen.MainScreen.Bounds);
			window.AddSubview(ScreenResolutionMatcher.BackgroundImage());

			AppManager.Current.StartUp();
			window.MakeKeyAndVisible ();
			
			return true;
		}
		
		private void _InitializeControllers() {
			_loginViewController = new LoginViewController();
			_communityViewController = new CommunityViewController();
			_cardsViewController = new CardsViewController();
			_userViewController = new UserViewController();
			_tutorialViewController = new TutorialViewController();
		}

		public bool IsNetworkAvailable() {
			return Reachability.IsRemoteHostReachable();
		}

		public void NetworkUsageStarted(bool blockUser, string title)
		{
			UIApplication.SharedApplication.NetworkActivityIndicatorVisible = true;
			if (!blockUser)
				return;
			
			InvokeOnMainThread(()=>{
				if (_loadingView==null)
					_loadingView = new LoadingHUDView();
				_loadingView.Title = title;
				UIApplication.SharedApplication.Windows.Last().AddSubview(_loadingView);	
				_loadingView.StartAnimating();
			});
		}
		
		public void NetworkUsageEnded ()
		{
			UIApplication.SharedApplication.NetworkActivityIndicatorVisible = false;
				if (_loadingView!=null){
					_loadingView.FadeOutAndRemove();
					_loadingView = null;
				}
		}
		
		public void ShowMessage(string title, string message) {
			InvokeOnMainThread(()=>{
				new UIAlertView(title, message, null, "OK" ,null).Show();
			});
		}
		
		public void ApplicationStartUpMode(AppMode mode) {
			switch (mode) {
				case AppMode.Login:
					Console.WriteLine("Launching login mode...");
					InvokeOnMainThread(()=>{
					_loginViewController.View.Frame = ScreenResolutionMatcher.FullViewFrame();
						_RemoveAllSubviews();
						window.AddSubview(_loginViewController.View);
					});
					break;
				
				case AppMode.Tabs:
					Console.WriteLine("Launching tabs mode...");

					AppManager.Current.CommunityService.GetCommunityStats(); //sync call
					AppManager.Current.CardsService.GetCards(); //sync call

					InvokeOnMainThread(()=>{
						var tabBarController = new UITabBarController();
						if (ScreenResolutionMatcher.IsiOS7()) {
							tabBarController.TabBar.BarTintColor = UIColor.Black;
							tabBarController.TabBar.TintColor = UIColor.White;
						}
						//tabBarController.Delegate = new ProdeTabBarDelegate(this);
						tabBarController.ViewControllers = new UIViewController [] {
							_communityViewController,
							_cardsViewController,
							_userViewController
						};
						_RemoveAllSubviews();
						window.RootViewController = tabBarController;
					});
					break;

				case AppMode.Newbie:
					Console.WriteLine("Launching newbie mode...");
					InvokeOnMainThread(()=>{
						_tutorialViewController.View.Frame = ScreenResolutionMatcher.FullViewFrame();
						_RemoveAllSubviews();
						window.AddSubview(_tutorialViewController.View);
					});
					break;
			}
		}
		
		private void _RemoveAllSubviews() {
			foreach (var view in window.Subviews){
	        	view.RemoveFromSuperview();
			}
		}
	}

	public class ProdeTabBarDelegate : UITabBarControllerDelegate {
		private UIClient _uiClient;
		public ProdeTabBarDelegate(UIClient client) {
			_uiClient = client;			
		}

		public override bool ShouldSelectViewController (UITabBarController tabBarController, UIViewController viewController) {
			Console.WriteLine("should select");

			_uiClient.NetworkUsageStarted(true, "Cargando");
			return true;
		}

		public override void ViewControllerSelected (UITabBarController tabBarController, UIViewController viewController) {
			_uiClient.NetworkUsageEnded();
			Console.WriteLine("vc selected");
		}
	}
}

