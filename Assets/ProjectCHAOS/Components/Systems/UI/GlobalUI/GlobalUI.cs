using UnityEngine;
using ProjectCHAOS.Systems.Inputs;
using ProjectCHAOS.Gameplay.HUD;
using ProjectCHAOS.Gameplay.Menus;

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

		public TouchUIController touchUiController => _touchUiController;

		public StartMenuUI startMenuUI => _startMenuUi;

		public HUDUI hudUi => _hudUi;
	}
}
