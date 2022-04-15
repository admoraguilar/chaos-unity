using UnityEngine;
using ProjectCHAOS.Gameplay.Scores;

namespace ProjectCHAOS.UI
{
	public class HUDUI : MonoBehaviour
	{
		[SerializeField]
		private ScoreUI _scoreUi = null;

		public ScoreUI scoreUi => _scoreUi;
	}
}

