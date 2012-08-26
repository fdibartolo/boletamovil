using System;
using MonoTouch.UIKit;
using MonoTouch.ObjCRuntime;
using MonoTouch.Foundation;
using System.Drawing;

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

	public static UIView FindNextTextFieldResponder(this UITextField textField) {
		var tagToFind = (textField.Tag > 0)
			? (textField.Tag * -1) 
			: (textField.Tag * -1) + 1;

		return textField.Superview.ViewWithTag(tagToFind);
    }

	public static void ShowDoneButtonOnKeyboard(this UITextField textFieldFirstResponder){  
		var keyboard = ViewExtensions.GetKeyBoardView();
		UIButton doneButton = UIButton.FromType(UIButtonType.Custom);
		doneButton.Frame = new RectangleF(0,163,106,53);
		doneButton.AdjustsImageWhenHighlighted = false;
		doneButton.SetImage(UIImage.FromFile("Images/DoneUp.png"),UIControlState.Normal);
		doneButton.SetImage(UIImage.FromFile("Images/DoneDown.png"),UIControlState.Highlighted);
		doneButton.TouchUpInside += delegate(object sender, EventArgs e) {
			textFieldFirstResponder.ResignFirstResponder();	
		};

		textFieldFirstResponder.EditingDidEnd += delegate(object sender, EventArgs e) { 
			// This doubles every time notification posts... need to only add once
			if(doneButton != null){
				doneButton.RemoveFromSuperview();
				doneButton = null;
			}
		};

		if(keyboard != null) {
			keyboard.AddSubview(doneButton);
		}
	}
}