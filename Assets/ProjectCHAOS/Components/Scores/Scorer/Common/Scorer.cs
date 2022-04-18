using System.Collections.Generic;
using UnityEngine;

namespace ProjectCHAOS.Scores
{
	public class Scorer : MonoBehaviour
	{
		[SerializeField]
		private List<Score> _scoreList = new List<Score>();

		public Score GetScore(int id)
		{
			Score result = _scoreList.Find(s => s.id == id);
			if(result == null) {
				result = new Score(id);
				_scoreList.Add(result);
			}
			return result;
		}

		public IReadOnlyList<Score> GetAllScores() => _scoreList;

		public void Clear()
		{
			if(_scoreList == null) {
				_scoreList = new List<Score>();
			}

			_scoreList.Clear();
		}
	}
}
