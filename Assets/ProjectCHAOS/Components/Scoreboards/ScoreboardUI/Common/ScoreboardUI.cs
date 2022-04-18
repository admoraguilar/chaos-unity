using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ProjectCHAOS.Scoreboards
{
	public class ScoreboardUI : MonoBehaviour
	{
		public event Action OnBackButtonPressed = delegate { };

		[SerializeField]
		private ScoreboardItemUI _itemUiPrefab = null;

		[SerializeField]
		private RectTransform _contentParent = null;

		[SerializeField]
		private Button _backButton = null;

		private List<ScoreboardItemUI> _itemUiInstances = new List<ScoreboardItemUI>();

		public void Populate(IEnumerable<ScoreObject> scores)
		{
			foreach(ScoreboardItemUI instance in _itemUiInstances) {
				Destroy(instance.gameObject);
			}

			int entryNumber = 1;
			foreach(ScoreObject score in scores) {
				ScoreboardItemUI itemUi = Instantiate(_itemUiPrefab, _contentParent);
				itemUi.gameObject.SetActive(true);

				itemUi.entryNumberText.text = $"{entryNumber}";
				itemUi.scoreText.text = $"{score.score}";
				itemUi.daysText.text = $"{score.days}";
				itemUi.wavesText.text = $"{score.waves}";

				entryNumber++;
			}
		}

		private void OnBackButtonClicked()
		{
			OnBackButtonPressed();
		}

		private void OnEnable()
		{
			_backButton.onClick.AddListener(OnBackButtonClicked);
		}

		private void OnDisable()
		{
			_backButton.onClick.RemoveListener(OnBackButtonClicked);
		}
	}
}
