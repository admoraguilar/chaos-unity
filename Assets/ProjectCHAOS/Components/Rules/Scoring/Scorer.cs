using UnityEngine;
using ProjectCHAOS.Utilities;

namespace ProjectCHAOS.Rules.Scores
{
	[CreateAssetMenu(menuName = "ProjectCHAOS/Scorer")]
	public class Scorer : ScriptableSingleton<Scorer>
	{
		[SerializeField]
		private int _score = 0;
		
		public int score
		{
			get => _score;
			set => _score = value;
		}

		// Add saving mechanism of the scores here like
		// best scores and save it to a file...
	}
}
