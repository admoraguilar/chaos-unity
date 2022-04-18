using UnityEngine;
using TMPro;

namespace ProjectCHAOS.Scoreboards
{
	public class ScoreboardItemUI : MonoBehaviour
	{
		[SerializeField]
		private TMP_Text _entryNumberText = null;

		[SerializeField]
		private TMP_Text _scoreText = null;

		[SerializeField]
		private TMP_Text _daysText = null;

		[SerializeField]
		private TMP_Text _wavesText = null;

		public TMP_Text entryNumberText => _entryNumberText;

		public TMP_Text scoreText => _scoreText;

		public TMP_Text daysText => _daysText;

		public TMP_Text wavesText => _wavesText;
	}
}
