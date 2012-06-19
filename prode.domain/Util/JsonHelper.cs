using System;
using System.Collections.Generic;

namespace prode.domain
{
	public static class JsonHelper
	{
		public static string ConvertJsonKeysToProperties(Dictionary<string, string> keyProp, string source) {
			string destination = source;
			foreach (var key in keyProp.Keys) {
				destination = source.Replace(key, keyProp[key]);
				source = destination;
			}
			return destination;
		}
	}
}

