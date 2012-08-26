using System;
using MonoTouch.UIKit;
using System.Drawing;
using System.Collections.Generic;
 
namespace prode
{
	public class PagedViewController : UIViewController
    {
    	public IPagedViewDataSource PagedViewDataSource { get; set; }
 
		readonly UIScrollView _scrollView = new PagedScrollView();
		readonly IList<UIViewController> _pages = new List<UIViewController>();
 
		readonly UIPageControl _pageControl = new UIPageControl {
			Pages = 0,
            Frame = new RectangleF(0, 396, 320, 14)
        };
           
		public PagedViewController() {
			_scrollView.DecelerationEnded += HandleScrollViewDecelerationEnded;
			_pageControl.ValueChanged += HandlePageControlValueChanged;

			_scrollView.DraggingStarted += HandleScrollViewDraggingStarted; 
		}
           
		private int _page;
		public int Page {
			get { return _page; }
			set {
				_pageControl.CurrentPage = value;
				_page = value;
				_scrollView.SetContentOffset(new PointF((value*320), 0), true);
				_pages[value].ViewDidAppear(true);
			}
		}
 
		void HandleScrollViewDecelerationEnded (object sender, EventArgs e) {
			int page = (int) Math.Floor((_scrollView.ContentOffset.X - _scrollView.Frame.Width / 2) / _scrollView.Frame.Width) + 1;
			_page = page;
			_pageControl.CurrentPage = page;
			_pages[page].ViewDidAppear(true);
		}
           
		void HandlePageControlValueChanged (object sender, EventArgs e) {
			Page = _pageControl.CurrentPage;
		}

		void HandleScrollViewDraggingStarted(object sender, EventArgs e) {
			var firstResponder = this.View.FindFirstResponder ();
			if (firstResponder != null)
				firstResponder.ResignFirstResponder ();
		}

		public void ReloadPages() {
			PagedViewDataSource.Reload();
                 
			foreach (var p in _pages)
				p.View.RemoveFromSuperview();
                 
				int i;
				var numberOfPages = PagedViewDataSource.Pages;
				for (i=0; i<numberOfPages; i++) {
					var pageViewController = PagedViewDataSource.GetPage(i);
					pageViewController.View.Frame = new RectangleF(320*i, 0, 320, this._scrollView.Frame.Height-30);
					_scrollView.AddSubview(pageViewController.View);
					_pages.Add(pageViewController);
				}
                 
				_scrollView.ContentSize = new SizeF(320*(i==0?1:i), 400);
				_pageControl.Pages = i;
				_pageControl.CurrentPage = 0;
                 
				PagedViewDataSource.Reload();
				_pages[0].ViewDidAppear(true);
			}
           
		public override void ViewDidLoad () {
			View.Frame = new RectangleF(0, 20, 320, 480);
			View.BackgroundColor = UIColor.Black;
			View.AddSubview(_scrollView);
			View.AddSubview(_pageControl);
		}
           
		public override void ViewDidAppear (bool animated) {
			ReloadPages();
		}
 
		sealed class PagedScrollView : UIScrollView {
			public PagedScrollView() {
				ShowsHorizontalScrollIndicator = false;
				ShowsVerticalScrollIndicator = false;
				Bounces = true;
				ContentSize = new SizeF(320, 400);
				PagingEnabled = true;
				Frame = new RectangleF(0, 0, 320, 426);
			}
		}
	}
     
	public interface IPagedViewDataSource {
		int Pages { get; }
		UIViewController GetPage (int i);
        void Reload();
	}
}