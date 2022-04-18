using UnityEngine;
using ProjectCHAOS.Inputs;
using ProjectCHAOS.HUD;
using ProjectCHAOS.Menus;
using ProjectCHAOS.Scoreboards;
using ProjectCHAOS.Scores;

namespace ProjectCHAOS.UI
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

		public TouchUIController touchUiController => _touchUiController;

		public StartMenuUI startMenuUI => _startMenuUi;

		public HUDUI hudUi => _hudUi;

		public ScoreboardUI scoreboardUi => _scoreboardUi;

		public void Initialize(LeanTouchInput leanTouchInput, Scorer scorer)
		{
			touchUiController.Initialize(leanTouchInput);
			startMenuUI.Initialize(leanTouchInput, scorer);
			hudUi.Initialize(scorer);
		}

		public void HideAllUI()
		{
			touchUiController.gameObject.SetActive(false);
			startMenuUI.gameObject.SetActive(false);
			hudUi.gameObject.SetActive(false);
			scoreboardUi.gameObject.SetActive(false);
		}
	}
}
