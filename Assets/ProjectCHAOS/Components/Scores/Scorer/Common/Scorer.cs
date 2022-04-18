using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using ProjectCHAOS.DataSerialization;

namespace ProjectCHAOS.Scores
{
	public class Scorer : MonoBehaviour
	{
		[SerializeField]
		private List<Score> _scoreList = new List<Score>();

		private DataSerializer _dataSerializer = new DataSerializer("scores");

		public Score GetScore(int id)
		{
			Score result = _scoreList.Find(s => s.id == id);
			if(result == null) {
				result = new Score(id);
				_scoreList.Add(result);
			}
			return result;
		}

		public void LoadData()
		{
			ScoreDataV1 data = new ScoreDataV1();
			data = _dataSerializer.LoadObjectVersion<ScoreDataV1>();
			foreach(ScoreDataV1.Score dataScore in data.scores) {
				Score score = GetScore(dataScore.id);
				score.InternalSet(dataScore.id, score.current, dataScore.best);
			}
		}

		public void SaveData()
		{
			ScoreDataV1 data = new ScoreDataV1();
			data.scores.AddRange(
				_scoreList.Select(
					s => new ScoreDataV1.Score { 
						id = s.id, 
						best = s.best 
					})
			);
			_dataSerializer.SaveObjectVersion(data);
		}

		public void ClearData()
		{
			_dataSerializer.Clear();
		}

		public void Clear()
		{
			if(_scoreList == null) {
				_scoreList = new List<Score>();
			}

			_scoreList.Clear();
		}
	}
}
