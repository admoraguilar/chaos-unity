using System;
using UnityEngine;
using WaterToolkit.Behave;

namespace ProjectCHAOS.Characters
{
	public interface ICharacterStats
	{
		public CharacterStats characterStats { get; }
	}

	[Serializable]
	public class CharacterStats
	{
		[SerializeField]
		private EntityStat _speed = new EntityStat(1f);

		[SerializeField]
		private EntityStat _fireRate = new EntityStat(1f);

		public EntityStat speed => _speed;

		public EntityStat fireRate => _fireRate;

#if UNITY_EDITOR

		public void Editor_OnValidate()
		{
			speed.current.baseValue = speed.current.baseValue;
			fireRate.current.baseValue = fireRate.current.baseValue;

			speed.current.OnValidate();
			speed.max.OnValidate();

			fireRate.current.OnValidate();
			fireRate.max.OnValidate();
		}

#endif
	}
}
