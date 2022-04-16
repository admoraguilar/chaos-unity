using System;
using UnityEngine;
using ProjectCHAOS.UI;
using ProjectCHAOS.Systems.Inputs;
using ProjectCHAOS.Gameplay.Scores;

using URandom = UnityEngine.Random;
using ProjectCHAOS.Gameplay.Scoreboards;

namespace ProjectCHAOS.Gameplay.GameModes.Survival
{
	[Serializable]
	public class SurvivalSystem
	{
		[Header("Systems")]
		[SerializeField]
		private LeanTouchInput _input = null;

		[Header("Gameplay")]
		[SerializeField]
		private Scorer _scorer = null;
		private Score _score = null;

		[Header("UIs")]
		[SerializeField]
		private GlobalUI _globalUi = null;

		private SurvivalWorld _world = null;

		public LeanTouchInput input => _input;

		public Scorer scorer => _scorer;

		public Score score
		{
			get => _score;
			private set => _score = value;
		}

		public GlobalUI globalUi => _globalUi;

		public SurvivalWorld world
		{
			get => _world;
			private set => _world = value;
		}

		public void OnOutsideVisit()
		{
			globalUi.hudUi.gameObject.SetActive(true);
		}

		public void OnOutsideDeadVisit()
		{
			globalUi.hudUi.gameObject.SetActive(false);

			globalUi.scoreboardUi.OnBackButtonPressed += OnScoreboardUiBackButtonPressed;

			ScoreObject scoreObj = new ScoreObject();
			scoreObj.name = "Test";
			scoreObj.score = score.current;
			scoreObj.days = 1;
			scoreObj.waves = 1;

			globalUi.scoreboardUi.Populate(new ScoreObject[] { scoreObj, scoreObj });
			globalUi.scoreboardUi.gameObject.SetActive(true);

			void OnScoreboardUiBackButtonPressed()
			{
				globalUi.scoreboardUi.gameObject.SetActive(false);
				score.Reset();
				globalUi.scoreboardUi.OnBackButtonPressed -= OnScoreboardUiBackButtonPressed;
			}
		}

		private void OnBulletHit(GameObject obj)
		{
			score.current += URandom.Range(1, 5);
		}

		public void Awake(SurvivalWorld world)
		{
			this.world = world;

			score = scorer.GetScore(0);
			globalUi.startMenuUI.Initialize(input, scorer);
			globalUi.hudUi.scoreUi.Initialize(scorer);

			globalUi.touchUiController.Initialize(input);
		}

		public void OnEnable()
		{
			world.OnBulletHit += OnBulletHit;
		}

		public void OnDisable()
		{
			world.OnBulletHit -= OnBulletHit;
		}
	}
}
