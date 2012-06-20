using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace prode.domain
{
	public class Community
	{
		public string TournamentName { get; set; }
		public string GroupName { get; set; }
		public List<RankedUser> Ranking { get; set; }
		
		public static List<Community> BuildListOfFromJson(string jsonString) {
			var keyProp = new Dictionary<string, string> {
				{"tournament_name","TournamentName"},	
				{"group_name","GroupName"},	
				{"ranking","Ranking"},
				{"position","Position"},
				{"nick_name","NickName"},
				{"points","Points"}
			};
			
			var convertedJsonString = JsonHelper.ConvertJsonKeysToProperties(keyProp, jsonString);
			var community = JsonConvert.DeserializeObject<List<Community>>(convertedJsonString);

			return community as List<Community>;
		}
	}
}


