using UnityEngine;
using ProjectCHAOS.Scores;

namespace ProjectCHAOS.HUD
{
	public class HUDUI : MonoBehaviour
	{
		[SerializeField]
		private ScoreUI _scoreUi = null;

		public ScoreUI scoreUi => _scoreUi;

		public void Initialize(Scorer scorer)
		{
			scoreUi.Initialize(scorer);
		}
	}
}

