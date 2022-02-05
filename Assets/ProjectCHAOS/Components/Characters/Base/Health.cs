using System;
using UnityEngine;

namespace ProjectCHAOS.Characters
{
	[Serializable]
	public class Health
	{
		public event Action OnHealthEmpty = delegate { };
		public event Action<float> OnHealthChanged = delegate { };

		[SerializeField]
		private float _starting = 0f;

		[SerializeField]
		private float _max = 100f;

		private float _current = 0f;
		private bool _wasEmpty = false;

		public float starting
		{
			get => _starting;
			set => _starting = value;
		}

		public float max
		{
			get => _max;
			set => _max = value;
		}

		public float current
		{
			get => _current;
			set {
				float prev = _current;
				_current = Mathf.Clamp(value, 0f, _max);

				if(!_wasEmpty && _current == 0f) {
					OnHealthEmpty();
					_wasEmpty = false;
				}

				if(_wasEmpty && _current > 0f) {
					_wasEmpty = true;
				}

				if(_current != prev) {
					float delta = (_current - Mathf.Abs(value)) * Mathf.Sign(value);
					OnHealthChanged(delta);
				}
			}
		}
	}
}
