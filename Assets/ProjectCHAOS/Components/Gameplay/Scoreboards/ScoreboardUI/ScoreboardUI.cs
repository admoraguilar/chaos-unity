using UnityEngine;

namespace ProjectCHAOS.Gameplay.Scoreboards
{
	public class ScoreboardUI : MonoBehaviour
	{
		[Header("External References")]
		[SerializeField]
		private ScoreboardMono _scoreboardMono = null;

		public void Initialize(ScoreboardMono scoreboardMono)
		{
			_scoreboardMono = scoreboardMono;
		}
	}
}
