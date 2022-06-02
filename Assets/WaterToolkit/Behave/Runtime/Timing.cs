using System;
using UnityEngine;

namespace WaterToolkit.Behave
{
	[Serializable]
	public class Timing
	{
		public event Action OnReachMax = delegate { };

		[SerializeField]
		private float _current = 0f;

		[SerializeField]
		private float _max = 1f;

		[SerializeField]
		private float _speedMultiplier = 1f;

		[SerializeField]
		private bool _isResetOnMaxSet = true;

		public float current
		{
			get => _current;
			set {
				_current = value;
				if(_current > max) {
					OnReachMax();
					_current = 0f;
				}
			}
		}

		public float max
		{
			get => _max;
			set {
				_max = value;
				if(_isResetOnMaxSet) {
					_current = 0f;
				}
			}
		}

		public float speedMultiplier
		{
			get => _speedMultiplier;
			set => _speedMultiplier = value;
		}

		public bool isResetOnMaxSet
		{
			get => _isResetOnMaxSet;
			set => _isResetOnMaxSet = value;
		}

		public void Update() 
		{
			current += _speedMultiplier * Time.deltaTime;
		}

		public void Reset()
		{
			current = 0f;
		}
	}
}

