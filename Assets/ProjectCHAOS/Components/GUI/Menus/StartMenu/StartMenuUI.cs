using System;
using UnityEngine;
using Lean.Touch;

namespace ProjectCHAOS.GUI.Menus
{
	public class StartMenuUI : MonoBehaviour
	{
		public event Action OnTouchScreen = delegate { };

		[SerializeField]
		private LeanFingerTap _fingerTap = null;

		public LeanFingerTap fingerTap
		{
			get => _fingerTap;
			private set => _fingerTap = value;
		}

		public void Initialize(LeanFingerTap fingerTap)
		{
			if(this.fingerTap != null) {
				this.fingerTap.OnFinger.RemoveListener(OnLeanFinger);
			}

			this.fingerTap = fingerTap;
			this.fingerTap.OnFinger.AddListener(OnLeanFinger);
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
