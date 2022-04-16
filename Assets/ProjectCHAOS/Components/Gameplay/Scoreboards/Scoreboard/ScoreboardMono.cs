using UnityEngine;

namespace ProjectCHAOS.Gameplay.Scoreboards
{
	public class ScoreboardMono : MonoBehaviour
	{
		[SerializeField]
		private Scoreboard _scoreboard = null;

		public Scoreboard scoreboard => _scoreboard;
	}
}
