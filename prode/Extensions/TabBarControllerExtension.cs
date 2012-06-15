using System;
using MonoTouch.UIKit;

namespace prode
{
	public static class TabBarControllerExtension
	{
		public static void SetTitle(this UITabBarController controller, string title) {
			
			foreach (var vc in controller.ViewControllers) {
				//vc.NavigationController.Title = title;
			}
		}
	}
}

