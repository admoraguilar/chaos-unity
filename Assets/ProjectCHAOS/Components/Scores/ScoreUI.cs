using UnityEngine;
using TMPro;

namespace ProjectCHAOS.Scores
{
	public class ScoreUI : MonoBehaviour
	{
		[SerializeField]
		private TMP_Text _text = null;

		private Score _score = null;

		private void OnScoreUpdate(int score)
		{
			_text.text = score.ToString();
		}

		private void Awake()
		{
			_score = Scorer.Instance.GetScore(0);
		}

		private void Start()
		{
			_text.text = "0";
		}

		private void OnEnable()
		{
			_score.OnUpdate += OnScoreUpdate;
		}

		private void OnDisable()
		{
			_score.OnUpdate -= OnScoreUpdate;
		}
	}
}
