using UnityEngine;

namespace ProjectCHAOS.Scoreboards
{
	public class ScoreboardMono : MonoBehaviour
	{
		[SerializeField]
		private Scoreboard _scoreboard = null;

		public Scoreboard scoreboard => _scoreboard;
	}
}
