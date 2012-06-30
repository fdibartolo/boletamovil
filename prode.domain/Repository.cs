using System;
using System.Collections.Generic;
using System.Linq;

namespace prode.domain
{
	public class Repository
	{
		public User User { get; set; }
		public List<Card> Cards { get; set; }
		public List<Community> CommunityStats { get; set; }

		public void UpdateHomeResultForMatch (int matchId, string result)
		{
			var match = _FindMatchById(matchId);

			int score;
			if (Int32.TryParse(result, out score))
				match.HomeUserScore = score;
			else
				match.HomeUserScore = null;
		}

		public void UpdateGuestResultForMatch (int matchId, string result)
		{
			var match = _FindMatchById(matchId);

			int score;
			if (Int32.TryParse(result, out score))
				match.GuestUserScore = score;
			else
				match.GuestUserScore = null;
		}

		private Match _FindMatchById (int id)
		{
			return (from c in Cards
						from m in c.Matches
						where (m.MatchId == id)
						select m).FirstOrDefault();
		}
	}
}

