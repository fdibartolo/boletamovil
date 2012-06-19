using System;
using System.Json;

namespace prode.domain
{
	public class Match
	{
		public int MatchId { get; set; }
		public string HomeTeam { get; set; }
		public string GuestTeam { get; set; }
		public int HomeRealScore { get; set; }
		public int GuestRealScore { get; set; }
		public int HomeUserScore { get; set; }
		public int GuestUserScore { get; set; }
	}
}
