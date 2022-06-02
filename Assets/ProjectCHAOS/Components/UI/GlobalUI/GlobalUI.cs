using UnityEngine;
using WaterToolkit.Inputs;
using WaterToolkit.Scores;
using WaterToolkit.Scoreboards;
using WaterToolkit.Upgrades;
using System.Collections.Generic;

namespace WaterToolkit.UI
{
	public class GlobalUI : MonoBehaviour
	{
		[SerializeField]
		private TouchUIController _touchUiController = null;

		[SerializeField]
		private StartMenuUI _startMenuUi = null;

		[SerializeField]
		private HUDUI _hudUi = null;

		[SerializeField]
		private ScoreboardUI _scoreboardUi = null;

		[SerializeField]
		private UpgraderUI _upgraderUi = null;

		public TouchUIController touchUiController => _touchUiController;

		public StartMenuUI startMenuUI => _startMenuUi;

		public HUDUI hudUi => _hudUi;

		public ScoreboardUI scoreboardUi => _scoreboardUi;

		public UpgraderUI upgraderUi => _upgraderUi;

		public void Initialize(
			LeanTouchInput leanTouchInput, Scorer scorer, 
			Upgrader upgrader)
		{
			touchUiController.Initialize(leanTouchInput);
			startMenuUI.Initialize(leanTouchInput, scorer);
			hudUi.Initialize(scorer);
			upgraderUi.Initialize(upgrader);
		}

		public void HideAllUI()
		{
			touchUiController.gameObject.SetActive(false);
			startMenuUI.gameObject.SetActive(false);
			hudUi.gameObject.SetActive(false);
			scoreboardUi.gameObject.SetActive(false);
			upgraderUi?.gameObject.SetActive(false);
		}
	}
}
