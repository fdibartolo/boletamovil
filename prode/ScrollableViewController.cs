using System;
using MonoTouch.UIKit;
using System.Drawing;
using MonoTouch.Foundation;

namespace prode
{
	public class ScrollableViewController : UIViewController
	{
		private NSObject _keyboardObserverWillShow;
		private NSObject _keyboardObserverWillHide;
		         
		public override void ViewDidLoad() {
		    base.ViewDidLoad ();
		 
		    // Setup keyboard event handlers
		    RegisterForKeyboardNotifications();
		}
		 
		public override void ViewDidUnload() {
		    base.ViewDidUnload();
		    UnregisterKeyboardNotifications();
		}
		
		protected virtual void RegisterForKeyboardNotifications () {
		    _keyboardObserverWillShow = NSNotificationCenter.DefaultCenter.AddObserver(UIKeyboard.DidShowNotification, KeyboardDidShowNotification);
		    _keyboardObserverWillHide = NSNotificationCenter.DefaultCenter.AddObserver(UIKeyboard.WillHideNotification, KeyboardWillHideNotification);
		}
		         
		protected virtual void UnregisterKeyboardNotifications() {
		    NSNotificationCenter.DefaultCenter.RemoveObserver(_keyboardObserverWillShow);
		    NSNotificationCenter.DefaultCenter.RemoveObserver(_keyboardObserverWillHide);
		}
		 
		protected virtual UIView KeyboardGetActiveView() {
		    return this.View.FindFirstResponder();
		}
		 
		protected virtual void KeyboardDidShowNotification (NSNotification notification) {
		    UIView activeView = KeyboardGetActiveView();
		    if (activeView == null)
		        return;

			_HandleDoneButtonOnKeyboard((UITextField)activeView);

		    UIScrollView scrollView = activeView.FindSuperviewOfType(this.View, typeof(UIScrollView)) as UIScrollView;
		    if (scrollView == null)
		        return;
		 
		    RectangleF keyboardBounds = UIKeyboard.BoundsFromNotification(notification);
		 
		    UIEdgeInsets contentInsets = new UIEdgeInsets(0.0f, 0.0f, keyboardBounds.Size.Height, 0.0f);
		    scrollView.ContentInset = contentInsets;
		    scrollView.ScrollIndicatorInsets = contentInsets;
		 
		    // If activeField is hidden by keyboard, scroll it so it's visible
		    RectangleF viewRectAboveKeyboard = new RectangleF(this.View.Frame.Location, new SizeF(this.View.Frame.Width, this.View.Frame.Size.Height - keyboardBounds.Size.Height));
		 
		    RectangleF activeFieldAbsoluteFrame = activeView.Superview.ConvertRectToView(activeView.Frame, this.View);
		    // activeFieldAbsoluteFrame is relative to this.View so does not include any scrollView.ContentOffset
		 
		    // Check if the activeField will be partially or entirely covered by the keyboard
		    if (!viewRectAboveKeyboard.Contains(activeFieldAbsoluteFrame)) {
		        // Scroll to the activeField Y position + activeField.Height + current scrollView.ContentOffset.Y - the keyboard Height
		        PointF scrollPoint = new PointF(0.0f, activeFieldAbsoluteFrame.Location.Y + activeFieldAbsoluteFrame.Height + scrollView.ContentOffset.Y - viewRectAboveKeyboard.Height);
		        scrollView.SetContentOffset(scrollPoint, true);
		    }
		}
		 
		protected virtual void KeyboardWillHideNotification (NSNotification notification) {
		    UIView activeView = KeyboardGetActiveView();
		    if (activeView == null)
		        return;
		 
		    UIScrollView scrollView = activeView.FindSuperviewOfType (this.View, typeof(UIScrollView)) as UIScrollView;
		    if (scrollView == null)
		        return;
		 
		    // Reset the content inset of the scrollView and animate using the current keyboard animation duration
		    double animationDuration = UIKeyboard.AnimationDurationFromNotification(notification);
		    UIEdgeInsets contentInsets = new UIEdgeInsets(0.0f, 0.0f, 0.0f, 0.0f);
		    UIView.Animate(animationDuration, delegate{
		        scrollView.ContentInset = contentInsets;
		        scrollView.ScrollIndicatorInsets = contentInsets;
		    });
		}	
		
		private void _HandleDoneButtonOnKeyboard(UITextField textFieldFirstResponder){  
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
}

