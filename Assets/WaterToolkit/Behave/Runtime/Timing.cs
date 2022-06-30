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
		private float _speed = 1f;

		[SerializeField]
		private bool _isResetOnMaxSet = true;

		private bool _isRunning = false;

		public float current
		{
			get => _current;
			set {
				_current = value;
				if(_current > max) {
					OnReachMax();
					Reset();
				}
			}
		}

		public float max
		{
			get => _max;
			set {
				_max = value;
				if(_isResetOnMaxSet) {
					Reset();
				}
			}
		}

		public float speedMultiplier
		{
			get => _speed;
			set => _speed = value;
		}

		public bool isResetOnMaxSet
		{
			get => _isResetOnMaxSet;
			set => _isResetOnMaxSet = value;
		}

		public void Run()
		{
			_isRunning = true;
		}

		public void Pause()
		{
			_isRunning = false;
		}

		public void Stop()
		{
			Reset(true);
		}

		public void Update() 
		{
			if(!_isRunning) { return; }
			current += speedMultiplier * Time.deltaTime;
		}

		public void Reset(bool shouldStop = false)
		{
			current = 0f;
			if(shouldStop) {
				_isRunning = false;
			}
		}
	}
}

