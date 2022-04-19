using System;
using UnityEngine;
using ProjectCHAOS.Behave;

namespace ProjectCHAOS.Upgrades
{
	[CreateAssetMenu(menuName = "ProjectCHAOS/Upgrades/Speed")]
	public class SpeedUpgradeObject : UpgradeObject<SpeedUpgradeBehaviour> { }

	[Serializable]
	public class SpeedUpgradeBehaviour : UpgradeBehaviour
	{
		public float speed = 1;

		public override bool Upgrade(Transform toUpgrade)
		{
			ICharacterStats characterStats = toUpgrade.GetComponentInParentAndChildren<ICharacterStats>();
			if(characterStats == null) {
				Debug.LogError("Speed upgrade can't work as there's no character stats");
				return false;
			}

			characterStats.characterStats.speed.value = speed;
			return true;
		}
	}
}
