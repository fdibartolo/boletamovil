using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace prode.domain
{
	public class Card
	{
		public int WeekId { get; set; }
		public string WeekName { get; set; }
		public string TournamentName { get; set; }
		public string WeekStatus { get; set; }
		public List<Match> Matches { get; set; }
		
		public static List<Card> BuildListOfFromJson(string jsonString) {
			var keyProp = new Dictionary<string, string> {
				{"week_id","WeekId"},	
				{"week_name","WeekName"},	
				{"tournament_name","TournamentName"},	
				{"week_status","WeekStatus"},	
				{"matches","Matches"},
				{"match_id","MatchId"},
				{"home_team","HomeTeam"},
				{"guest_team","GuestTeam"},
				{"home_real_score","HomeRealScore"},
				{"guest_real_score","GuestRealScore"},
				{"home_user_score","HomeUserScore"},
				{"guest_user_score","GuestUserScore"}
			};
			
			var convertedJsonString = JsonHelper.ConvertJsonKeysToProperties(keyProp, jsonString);
			var cards = JsonConvert.DeserializeObject<List<Card>>(convertedJsonString);

			return cards as List<Card>;
		}
	}
}

