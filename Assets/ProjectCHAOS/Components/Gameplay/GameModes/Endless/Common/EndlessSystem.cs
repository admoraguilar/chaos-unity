using System;
using UnityEngine;
using ProjectCHAOS.UI;
using ProjectCHAOS.Systems.Inputs;
using ProjectCHAOS.Gameplay.Scores;

using URandom = UnityEngine.Random;

namespace ProjectCHAOS.Gameplay.GameModes.Endless
{
	[Serializable]
	public class EndlessSystem : GameSystem<EndlessWorld>
	{
		public event Action OnStartMenuPressAnywhere = delegate { };

		[SerializeField]
		private Scorer _scorer = null;
		private Score _score = null;

		[SerializeField]
		private LeanTouchInput _leanTouchInput = null;

		[SerializeField]
		private GlobalUI _globalUi = null;

		public void OnInitializeVisit()
		{
			_globalUi.HideAllUI();
		}

		public void OnStartMenuVisit()
		{
			_globalUi.startMenuUI.gameObject.SetActive(true);
		}

		public void OnStartMenuLeave()
		{
			_globalUi.startMenuUI.gameObject.SetActive(false);
			_score.Reset();
		}

		public void OnGameVisit()
		{
			_globalUi.hudUi.gameObject.SetActive(true);
			_globalUi.touchUiController.gameObject.SetActive(true);
		}

		public void OnGameLeave()
		{
			_globalUi.hudUi.gameObject.SetActive(false);
			_globalUi.touchUiController.gameObject.SetActive(false);
		}

		public void OnGameOverVisit()
		{
			_globalUi.touchUiController.SimulateOnPointerUp();
		}

		private void OnBulletHitEnemy()
		{
			_score.current += URandom.Range(1, 5);
		}

		private void OnStartMenuPressedAnywhereMethod()
		{
			OnStartMenuPressAnywhere();
		}

		protected override void OnInitialize(EndlessWorld world)
		{
			_globalUi.Initialize(_leanTouchInput, _scorer);

			_score = _scorer.GetScore(0);
		}

		protected override void OnDoEnable()
		{
			world.OnBulletHitEnemy += OnBulletHitEnemy;

			_globalUi.startMenuUI.OnPressAnywhere += OnStartMenuPressedAnywhereMethod;
		}

		protected override void OnDoDisable()
		{
			world.OnBulletHitEnemy -= OnBulletHitEnemy;

			_globalUi.startMenuUI.OnPressAnywhere -= OnStartMenuPressedAnywhereMethod;
		}
	}
}
