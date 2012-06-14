// WARNING
//
// This file has been generated automatically by MonoDevelop to store outlets and
// actions made in the Xcode designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using MonoTouch.Foundation;

namespace prode
{
	[Register ("LoginViewController")]
	partial class LoginViewController
	{
		[Outlet]
		MonoTouch.UIKit.UITextField txtUsername { get; set; }

		[Outlet]
		MonoTouch.UIKit.UITextField txtPassword { get; set; }

		[Action ("Login:")]
		partial void Login (MonoTouch.UIKit.UIButton sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (txtUsername != null) {
				txtUsername.Dispose ();
				txtUsername = null;
			}

			if (txtPassword != null) {
				txtPassword.Dispose ();
				txtPassword = null;
			}
		}
	}
}
