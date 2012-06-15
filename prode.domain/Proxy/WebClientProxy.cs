using System;
using System.Json;
using System.Net;
using System.Text;

namespace prode.domain
{
	public class WebClientProxy
	{
		private WebClient _webClient;
		
		public WebClientProxy() { _webClient = new WebClient(); }
		
		public string HttpGet(string url) {
			
			/// try!!! 401!!

			Uri uri = new Uri(url);
			byte[] bytes = _webClient.DownloadData(uri);
			return Encoding.UTF8.GetString(bytes);
		}
	}
}

