using System;
using UnityEngine;

namespace ProjectCHAOS.Gameplay.Behave
{
	[Serializable]
	public class CharacterStats
	{
		[SerializeField]
		private CharacterStat _speed = new CharacterStat(1f);

		[SerializeField]
		private CharacterStat _fireRate = new CharacterStat(1f);

		public CharacterStat speed => _speed;

		public CharacterStat fireRate => _fireRate;
	}

	[Serializable]
	public class CharacterStat
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

		public CharacterStat(float value)
		{
			this.value = value;
		}

		public void Initialize(MonoBehaviour mono)
		{
			_mono = mono;
		}
	}
}
