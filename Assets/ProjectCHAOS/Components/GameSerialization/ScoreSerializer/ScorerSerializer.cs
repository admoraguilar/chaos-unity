using System.Linq;
using ProjectCHAOS.Scores;

namespace ProjectCHAOS.GameSerialization
{
	public class ScorerSerializer : GameDataSerializer
	{
		private Scorer _scorer = null;

		public ScorerSerializer(string fileName, Scorer scorer) :
			base(string.Empty, fileName)
		{
			_scorer = scorer;
		}

		public override void Load()
		{
			ScoreDataV1 data = dataSerializer.LoadObjectVersion<ScoreDataV1>();
			foreach(ScoreDataV1.Score dataScore in data.scores) {
				Score score = _scorer.GetScore(dataScore.id);
				score.InternalSet(dataScore.id, score.current, dataScore.best);
			}
		}

		public override void Save()
		{
			ScoreDataV1 data = new ScoreDataV1();
			data.scores.AddRange(
				_scorer.GetAllScores().Select(
					s => new ScoreDataV1.Score {
						id = s.id,
						best = s.best
					})
			);
			dataSerializer.SaveObjectVersion(data);
		}
	}
}
