using System;
using System.Json;

namespace prode.domain
{
	public class User
	{
		public string NickName { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		
		public string FormattedName {
			get { return string.Format ("{0} {1}", FirstName, LastName); }
		}

		public static User BuildFromJson(string jsonString) {
			JsonValue jsonUser = JsonValue.Parse(jsonString);
			
			return new User() {
				NickName = ((JsonObject)jsonUser)["nick_name"],
				FirstName = ((JsonObject)jsonUser)["first_name"],
				LastName = ((JsonObject)jsonUser)["last_name"]
			};
		}
	}
}

