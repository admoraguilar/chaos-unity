using System;
using UnityEngine;
using Lean.Touch;

namespace ProjectCHAOS.Menus
{
	public class StartMenuUI : MonoBehaviour
	{
		public event Action OnTouchScreen = delegate { };

		[SerializeField]
		private LeanFingerTap _fingerTap = null;

		private void OnLeanFinger(LeanFinger finger)
		{
			OnTouchScreen();
		}

		private void OnEnable()
		{
			_fingerTap.OnFinger.AddListener(OnLeanFinger);
		}

		private void OnDisable()
		{
			_fingerTap.OnFinger.RemoveListener(OnLeanFinger);
		}
	}
}
