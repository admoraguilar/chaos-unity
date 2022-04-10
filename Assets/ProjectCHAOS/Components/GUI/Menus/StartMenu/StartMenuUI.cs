using System;
using UnityEngine;
using Lean.Touch;
using ProjectCHAOS.Scores;

namespace ProjectCHAOS.GUI.Menus
{
	public class StartMenuUI : MonoBehaviour
	{
		public event Action OnTouchScreen = delegate { };

		[SerializeField]
		private LeanFingerTap _fingerTap = null;

		[SerializeField]
		private Scorer _scorer = null;

		[SerializeField]
		private ScoreUI _currentScoreUI = null;

		[SerializeField]
		private ScoreUI _bestScoreUI = null;

		public LeanFingerTap fingerTap
		{
			get => _fingerTap;
			private set => _fingerTap = value;
		}

		public Scorer scorer
		{
			get => _scorer;
			private set => _scorer = value;
		}

		public void Initialize(LeanFingerTap fingerTap, Scorer scorer)
		{
			if(this.fingerTap != null) {
				this.fingerTap.OnFinger.RemoveListener(OnLeanFinger);
			}

			this.fingerTap = fingerTap;
			this.fingerTap.OnFinger.AddListener(OnLeanFinger);

			this.scorer = scorer;

			_currentScoreUI.Initialize(this.scorer);
			_bestScoreUI.Initialize(this.scorer);
		}

		private void OnLeanFinger(LeanFinger finger)
		{
			OnTouchScreen();
		}

		private void OnEnable()
		{
			if(fingerTap != null) { fingerTap.OnFinger.AddListener(OnLeanFinger); }
		}

		private void OnDisable()
		{
			if(fingerTap != null) { fingerTap.OnFinger.RemoveListener(OnLeanFinger); }
		}
	}
}
