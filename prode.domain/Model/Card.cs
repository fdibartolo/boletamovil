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

			string json = "[{\"week_id\":1,\"tournament_name\":\"Primera A - Apertura 2012\",\"week_name\":\"Fecha: 1 - (28/09/12)\",\"week_status\":\"Opened\",\"matches\":[{\"home_team\":\"Independiente\",\"guest_team\":\"Boca Juniors\",\"home_real_score\":3,\"guest_real_score\":1,\"home_user_score\":3,\"guest_user_score\":1},{\"home_team\":\"Racing Club\",\"guest_team\":\"Velez Sarsfield\",\"home_real_score\":0,\"guest_real_score\":2,\"home_user_score\":0,\"guest_user_score\":0}]}]";
			//string json = "[{\"WeekId\":1,\"TournamentName\":\"Primera A - Apertura 2012\",\"WeekName\":\"Fecha: 1 - (28/09/12)\",\"WeekStatus\":\"Opened\",\"Matches\":[{\"HomeTeam\":\"Independiente\",\"GuestTeam\":\"Boca Juniors\",\"HomeRealScore\":3,\"GuestRealScore\":1,\"HomeUserScore\":3,\"GuestUserScore\":1},{\"HomeTeam\":\"Racing Club\",\"GuestTeam\":\"Velez Sarsfield\",\"HomeRealScore\":0,\"GuestRealScore\":2,\"HomeUserScore\":0,\"GuestUserScore\":0}]}]";
			
			var keyProp = new Dictionary<string, string> {
				{"week_id","WeekId"},	
				{"tournament_name","WeekName"},	
				{"week_name","TournamentName"},	
				{"week_status","WeekStatus"},	
				{"matches","Matches"},
				{"home_team","HomeTeam"},
				{"guest_team","GuestTeam"},
				{"home_real_score","HomeRealScore"},
				{"guest_real_score","GuestRealScore"},
				{"home_user_score","HomeUserScore"},
				{"guest_user_score","GuestUserScore"}
			};
			
			jsonString = JsonHelper.ConvertJsonKeysToProperties(keyProp, json);
			var cards = JsonConvert.DeserializeObject<List<Card>>(jsonString);

			return cards as List<Card>;
		}
	}
}

