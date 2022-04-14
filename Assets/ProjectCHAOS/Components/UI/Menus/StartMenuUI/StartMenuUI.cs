using System;
using UnityEngine;
using Lean.Touch;
using ProjectCHAOS.Systems.Inputs;
using ProjectCHAOS.Gameplay.Scores;

namespace ProjectCHAOS.UI.Menus
{
	public class StartMenuUI : MonoBehaviour
	{
		public event Action OnTouchScreen = delegate { };

		[SerializeField]
		private ScoreUI _currentScoreUI = null;

		[SerializeField]
		private ScoreUI _bestScoreUI = null;

		[Header("External References")]
		[SerializeField]
		private LeanTouchInput _leanTouchInput = null;

		[SerializeField]
		private Scorer _scorer = null;

		public LeanTouchInput leanTouchInput
		{
			get => _leanTouchInput;
			private set => _leanTouchInput = value;
		}

		public Scorer scorer
		{
			get => _scorer;
			private set => _scorer = value;
		}

		public void Initialize(LeanTouchInput leanTouchInput, Scorer scorer)
		{
			if(leanTouchInput != null) {
				LeanFingerTap tap = this.leanTouchInput.tap;
				if(this.leanTouchInput != null) {
					tap.OnFinger.RemoveListener(OnLeanFinger);
				}

				this.leanTouchInput = leanTouchInput;
				tap = this.leanTouchInput.tap;
				tap.OnFinger.AddListener(OnLeanFinger);
			}

			if(scorer != null) {
				this.scorer = scorer;

				_currentScoreUI.Initialize(this.scorer);
				_bestScoreUI.Initialize(this.scorer);
			}	
		}

		private void OnLeanFinger(LeanFinger finger)
		{
			OnTouchScreen();
		}

		private void OnEnable()
		{
			if(leanTouchInput != null) {
				leanTouchInput.tap.OnFinger.AddListener(OnLeanFinger);
			}
		}

		private void OnDisable()
		{
			if(leanTouchInput != null) {
				leanTouchInput.tap.OnFinger.RemoveListener(OnLeanFinger);
			}
		}
	}
}
