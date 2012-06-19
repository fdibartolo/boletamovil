using System;
using System.Json;
using System.Net;
using System.Text;
using System.Collections.Generic;
using prode.domain.constants;

namespace prode.domain
{
	public delegate void HttpGetCompleted(List<string> errors, string result);

	public class WebClientProxy
	{
		private WebClient _webClient;
		public HttpGetCompleted OnHttpGetCompleted;

		public WebClientProxy() { _webClient = new WebClient(); }

		public string HttpGet(string url) {
			Uri uri = new Uri(url);
			byte[] bytes = _webClient.DownloadData(uri);
			return Encoding.UTF8.GetString(bytes);
		}
		
		public void HttpGetAsync(string url) {
			Uri uri = new Uri(url);
			_webClient.DownloadDataCompleted += _HandleDownloadDataCompleted;
			_webClient.DownloadDataAsync(uri);
		}

		void _HandleDownloadDataCompleted (object sender, DownloadDataCompletedEventArgs e)
		{
			if (e.Error != null) {
				if (e.Error.Message.Contains("401"))
					OnHttpGetCompleted(new List<string> { Constants.ERROR_INVALID_CREDENTIALS }, null);
				else
					OnHttpGetCompleted(new List<string> { e.Error.Message }, null);
			}
			else 
				OnHttpGetCompleted(null, Encoding.UTF8.GetString(e.Result));
		}
	}
}

