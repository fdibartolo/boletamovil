using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace prode.domain
{
	public class User
	{
		public string NickName { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Password { get; set; }
		public bool Newbie { get; set; }
		
		public string FormattedName {
			get { return string.Format ("{0} {1}", FirstName, LastName); }
		}

		public bool IsValid() {
			return !String.IsNullOrEmpty(NickName) && !String.IsNullOrEmpty(Password);
		}

		public User Sanitize() {
			Password = "";
			return this;
		}		
		
		public static User BuildFromJson(string jsonString) {
			var keyProp = new Dictionary<string, string> {
				{"nick_name","NickName"},	
				{"first_name","FirstName"},	
				{"last_name","LastName"},	
				{"newbie","Newbie"}
			};
			
			var convertedJsonString = JsonHelper.ConvertJsonKeysToProperties(keyProp, jsonString);
			var user = JsonConvert.DeserializeObject<User>(convertedJsonString);

			return user as User;
		}
	}
}

