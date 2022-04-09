using UnityEngine;
using TMPro;

namespace ProjectCHAOS.Scores
{
	public class ScoreUI : MonoBehaviour
	{
		[SerializeField]
		private TMP_Text _currentText = null;

		[SerializeField]
		private TMP_Text _bestText = null;

		[SerializeField]
		private Scorer _scorer = null;

		private Score _score = null;

		private void OnScoreUpdate(int score)
		{
			if(_currentText == null) { return; }
			_currentText.text = score.ToString();
		}

		private void OnScoreNewBest(int newBestScore)
		{
			if(_bestText == null) { return; }
			_bestText.text = newBestScore.ToString();
		}

		private void Awake()
		{
			_score = _scorer.GetScore(0);
		}

		private void OnEnable()
		{
			if(_currentText != null) { 
				_score.OnUpdate += OnScoreUpdate;
				_currentText.text = _score.current.ToString();
			}
			if(_bestText != null) { 
				_score.OnNewBest += OnScoreNewBest;
				_bestText.text = _score.best.ToString();
			}
		}

		private void OnDisable()
		{
			if(_currentText != null) { _score.OnUpdate -= OnScoreUpdate; }
			if(_bestText != null) { _score.OnNewBest -= OnScoreNewBest; }
		}
	}
}
