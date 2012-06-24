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
		public string WeekDueDate { get; set; }
		public int Points { get; set; }
		public List<Match> Matches { get; set; }
		
		public static List<Card> BuildListOfFromJson(string jsonString) {
			var keyProp = new Dictionary<string, string> {
				{"week_id","WeekId"},	
				{"week_name","WeekName"},	
				{"tournament_name","TournamentName"},	
				{"due_date","WeekDueDate"},	
				{"points","Points"},	
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

		public bool IsEditable()
		{
			DateTime dueDate;
			return ((DateTime.TryParse(WeekDueDate, out dueDate)) && (dueDate > DateTime.Now));
		}

		public string ToJson()
		{
			var result = JsonConvert.SerializeObject(JsonCard.BuildFromCard(this));
			return "{\"card\":" + result + "}";
		}
		
		private sealed class JsonCard {
			public int week_id { get; set; }
			public List<JsonMatch> matches { get; set; }

			public static JsonCard BuildFromCard(Card card) {
				List<JsonMatch> jsonMatches = new List<JsonMatch>();
				foreach (var match in card.Matches) {
					jsonMatches.Add(new JsonMatch { 
						match_id = match.MatchId,
						home_score = match.HomeUserScore,
						guest_score = match.GuestUserScore });
				}
				
				return new JsonCard { week_id = card.WeekId, matches = jsonMatches };
			}
		}

		private sealed class JsonMatch {
			public int match_id { get; set; }
			public int? home_score { get; set; }
			public int? guest_score { get; set; }
		}
	}
}

