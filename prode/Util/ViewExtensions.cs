using System;
using MonoTouch.UIKit;

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
		Console.WriteLine ("X times");
        if (view.Superview != null) {
			Console.WriteLine ("hay superview");
            if (type.IsAssignableFrom(view.Superview.GetType())) {
				Console.WriteLine ("Y times");
                return view.Superview;
			}
			
            if (view.Superview != stopAt){
        		Console.WriteLine ("Z times");
		        return view.Superview.FindSuperviewOfType(stopAt, type);
			}
        }
 
		Console.WriteLine ("Not Found scrollable view: null");

        return null;
    }
}