using System;
using System.Json;
using System.Net;
using System.Text;

namespace prode.domain
{
	public class WebClientProxy
	{
		private WebClient _webClient;
		
		public WebClientProxy() { 
			_webClient = new WebClient();
			//_webClient.DownloadDataCompleted += _HandleDownloadDataCompleted;
		}

		public string HttpGet(string url) {
			Uri uri = new Uri(url);
			byte[] bytes = _webClient.DownloadData(uri);
			return Encoding.UTF8.GetString(bytes);
		}

//		public void HttpGetAsync(string url) {
//			
//			/// try!!! 401!!
//
//			Uri uri = new Uri(url);
//			_webClient.DownloadDataAsync(uri);
//		}
//		
//		private void _HandleDownloadDataCompleted (object sender, DownloadDataCompletedEventArgs e)
//		{
//			//if error sarasa
//			byte[] result = e.Result;
//			return Encoding.UTF8.GetString(result);
//		}
//		

	}
}

