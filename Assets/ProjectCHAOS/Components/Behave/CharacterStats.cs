using System;
using UnityEngine;

namespace ProjectCHAOS.Behave
{
	public interface ICharacterStats
	{
		public CharacterStats characterStats { get; }
	}

	[Serializable]
	public class CharacterStats
	{
		[SerializeField]
		private CharacterStat _speed = new CharacterStat(1f);

		[SerializeField]
		private CharacterStat _fireRate = new CharacterStat(1f);

		public CharacterStat speed => _speed;

		public CharacterStat fireRate => _fireRate;

#if UNITY_EDITOR
		public void Editor_OnValidate()
		{
			speed.value = speed.value;
			fireRate.value = fireRate.value;
		}
#endif
	}
}
