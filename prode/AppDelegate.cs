using System;
using System.Collections.Generic;
using System.Linq;

using MonoTouch.Foundation;
using MonoTouch.UIKit;
using prode.domain;

namespace prode
{
	// The UIApplicationDelegate for the application. This class is responsible for launching the 
	// User Interface of the application, as well as listening (and optionally responding) to 
	// application events from iOS.
	[Register ("AppDelegate")]
	public partial class AppDelegate : UIApplicationDelegate, UIClient
	{
		// class-level declarations
		UIWindow window;
		private LoadingHUDView _loadingView;
		private UIViewController LoginViewController;
		private UIViewController CommunityViewController;
		private UIViewController CardsViewController;
		private UIViewController UserViewController;

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
						
			//window.RootViewController = new LoginViewController();
			AppManager.Current.StartUp();
			window.MakeKeyAndVisible ();
			
			return true;
		}
		
		private void _InitializeControllers() {
			LoginViewController = new LoginViewController();
			CommunityViewController = new CommunityViewController();
			CardsViewController = new CardsViewController();
			UserViewController = new UserViewController();
		}
		
		public bool IsNetworkAvailable () {
			return Reachability.IsRemoteHostReachable();
		}

		public void NetworkUsageStarted (bool blockUser)
		{
			UIApplication.SharedApplication.NetworkActivityIndicatorVisible = true;
			if (!blockUser)
				return;
			
			InvokeOnMainThread(()=>{
				if (_loadingView==null)
					_loadingView = new LoadingHUDView();
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
					LoginViewController.View.Frame = new System.Drawing.RectangleF(0,20,320,460);
					InvokeOnMainThread(()=>{
						window.BackgroundColor = UIColor.White;
						_RemoveAllSubviews();
						window.AddSubview(LoginViewController.View);
					});
					break;
				
				case AppMode.Tabs:
					Console.WriteLine("Launching tabs mode...");
					var tabBarController = new UITabBarController();
					tabBarController.ViewControllers = new UIViewController [] {
						CommunityViewController,
						CardsViewController,
						UserViewController
					};
					InvokeOnMainThread(()=>{
						window.BackgroundColor = UIColor.White;
						_RemoveAllSubviews();
						window.RootViewController = tabBarController;
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

