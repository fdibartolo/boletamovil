using System;
using System.Json;
using System.Net;
using System.Collections.Generic;
using prode.domain.constants;

namespace prode.domain
{
	public delegate void HttpGetCompleted(List<string> errors, string result);
	public delegate void HttpPostCompleted(List<string> errors);

	public class WebClientProxy
	{
		private WebClient _webClient;
		public HttpGetCompleted OnHttpGetCompleted;
		public HttpPostCompleted OnHttpPostCompleted;
		
		public WebClientProxy() { _webClient = new WebClient(); }

		public void HttpGetAsync(string url) {
			Uri uri = new Uri(url);
			_webClient.DownloadStringCompleted += _HandleDownloadStringCompleted;
			_webClient.DownloadStringAsync(uri);
		}

		void _HandleDownloadStringCompleted (object sender, DownloadStringCompletedEventArgs e)
		{
			if (e.Error != null) {
				if (e.Error.Message.Contains("401"))
					OnHttpGetCompleted(new List<string> { Constants.ERROR_INVALID_CREDENTIALS }, null);
				else
					OnHttpGetCompleted(new List<string> { e.Error.Message }, null);
			}
			else 
				OnHttpGetCompleted(null, e.Result);
		}
		
		public void HttpPostAsync(string url, string data) {
			Uri uri = new Uri(url);
			_webClient.UploadStringCompleted += _HandleUploadStringCompleted;
			_webClient.Headers.Add(HttpRequestHeader.ContentType, "application/json");
			_webClient.UploadStringAsync(uri, "POST", data);
		}

		void _HandleUploadStringCompleted(object sender, UploadStringCompletedEventArgs e) {
			if (e.Error != null) {
				if (e.Error.Message.Contains("401"))
					OnHttpPostCompleted(new List<string> { Constants.ERROR_INVALID_CREDENTIALS });
				else
					OnHttpPostCompleted(new List<string> { e.Error.Message });
			}
			else 
				OnHttpPostCompleted(null);
		}
	}
}

