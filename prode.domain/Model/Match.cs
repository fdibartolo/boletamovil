using System;

namespace prode.domain
{
	public class Match
	{
		public int MatchId { get; set; }
		public string HomeTeam { get; set; }
		public string GuestTeam { get; set; }
		public string WeekDueDate { get; set; }
		public string GroupKey { get; set; }
		public int? HomeRealScore { get; set; }
		public int? GuestRealScore { get; set; }
		public int? HomeUserScore { get; set; }
		public int? GuestUserScore { get; set; }

		public bool IsEditable() {
			DateTime dueDate;
			return ((DateTime.TryParse(WeekDueDate, out dueDate)) && (dueDate > DateTime.Now));
		}

		public bool HasRealScore() {
			return HomeRealScore.HasValue && GuestRealScore.HasValue;
		}
	}
}
