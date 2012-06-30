using System;
using MonoTouch.UIKit;
using MonoTouch.ObjCRuntime;
using MonoTouch.Foundation;

public static class ViewExtensions
{
    public static UIView FindFirstResponder(this UIView view) {
        if (view.IsFirstResponder)
            return view;

		foreach (UIView subView in view.Subviews) {
            var firstResponder = subView.FindFirstResponder();
            if (firstResponder != null)
                return firstResponder;
        }
		
        return null;
    }
 
    public static UIView FindSuperviewOfType(this UIView view, UIView stopAt, Type type) {
        if (view.Superview != null) {
            if (type.IsAssignableFrom(view.Superview.GetType())) 
                return view.Superview;
			
            if (view.Superview != stopAt)
		        return view.Superview.FindSuperviewOfType(stopAt, type);
        }
 
        return null;
    }
	
	public static UIView GetKeyBoardView() {
		UIWindow window = UIApplication.SharedApplication.Windows[1];
		IntPtr sel = Selector.GetHandle ("description");
		foreach (var view in window.Subviews) {
			NSString desc = (NSString)Runtime.GetNSObject(Messaging.IntPtr_objc_msgSend(view.Handle, sel));
			if (desc.ToString().StartsWith("<UIPeripheralHostView")) {
				return view;
			}
		}
		return null;
	}
}