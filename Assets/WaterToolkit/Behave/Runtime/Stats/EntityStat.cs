using System;
using UnityEngine;

namespace WaterToolkit.Behave
{
	[Serializable]
	public class EntityStat
	{
		[SerializeField]
		private Stat<float> _current = new Stat<float>(1f);

		[SerializeField]
		private Stat<float> _max = new Stat<float>(100f);

		public Stat<float> current => _current;

		public Stat<float> max => _max;

		public EntityStat(float value)
		{
			_current.baseValue = value;
			Initialize();
		}

		public void Initialize()
		{
			_current.Initialize(
				_current.baseValue,
				null,
				(float inValue) => Mathf.Clamp(inValue, 0f, _max.baseValue),
				(float inValue) => Mathf.Clamp(inValue, 0f, _max.modifiedValue));

			_max.Initialize(_max.baseValue);
		}
	}
}
