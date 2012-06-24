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
			window.AddSubview(new UIImageView(UIImage.FromFile("Default.png")));

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
		
		public void ShowMessage (string title, string message)
		{
			InvokeOnMainThread(()=>{
				var popup = new UIAlertView(title, message, null, "OK" ,null);
				popup.Show();
			});
		}
		
		public void ApplicationStartUpMode(AppMode mode) {
			
			switch (mode) {
				case AppMode.Login:
					Console.WriteLine("Launching login mode...");
					_loginViewController.View.Frame = new System.Drawing.RectangleF(0,20,320,460);
					InvokeOnMainThread(()=>{
						_RemoveAllSubviews();
						window.AddSubview(_loginViewController.View);
					});
					break;
				
				case AppMode.Tabs:
					Console.WriteLine("Launching tabs mode...");

					AppManager.Current.CommunityService.GetCommunityStats(); //sync call
					AppManager.Current.CardsService.GetCards(); //sync call

					var tabBarController = new UITabBarController();
					tabBarController.ViewControllers = new UIViewController [] {
						_communityViewController,
						_cardsViewController,
						_userViewController
					};
					InvokeOnMainThread(()=>{
						_RemoveAllSubviews();
						window.RootViewController = tabBarController;
					});
					break;

				case AppMode.Newbie:
					Console.WriteLine("Launching newbie mode...");
					_tutorialViewController.View.Frame = new System.Drawing.RectangleF(0,20,320,460);
					InvokeOnMainThread(()=>{
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
}

