using System;
using UnityEngine;

namespace WaterToolkit.Behave
{
	[Serializable]
	public class EntityStat
	{
		public event Action<float> OnValueChange = delegate { };

		[SerializeField]
		private float _value = 1f;

		[SerializeField]
		private float _max = 100f;

		private MonoBehaviour _mono = null;

		public float value
		{
			get => _value;
			set {
				_value = Mathf.Clamp(value, 1f, float.PositiveInfinity);
				OnValueChange(value);
			}
		}

		public float max
		{
			get => _max;
			set => _max = value;
		}

		public EntityStat(float value)
		{
			this.value = value;
		}

		public void Initialize(MonoBehaviour mono)
		{
			_mono = mono;
		}
	}
}
