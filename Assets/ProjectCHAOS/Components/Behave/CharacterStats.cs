using System;
using UnityEngine;

namespace WaterToolkit.Behave
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
			speed.value = speed.value;
			fireRate.value = fireRate.value;
		}
#endif
	}
}
