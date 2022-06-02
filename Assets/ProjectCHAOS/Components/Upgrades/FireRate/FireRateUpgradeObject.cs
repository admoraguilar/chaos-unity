using System;
using UnityEngine;
using WaterToolkit.Behave;

namespace WaterToolkit.Upgrades
{
	[CreateAssetMenu(menuName = "ProjectCHAOS/Upgrades/Fire Rate")]
	public class FireRateUpgradeObject : UpgradeObject<FireRateUpgradeBehaviour> { }

	[Serializable]
	public class FireRateUpgradeBehaviour : UpgradeBehaviour
	{
		public float fireRate = 1;

		public override bool Upgrade(Transform toUpgrade)
		{
			ICharacterStats characterStats = toUpgrade.GetComponentInParentAndChildren<ICharacterStats>();
			if(characterStats == null) {
				Debug.LogError("Fire rate upgrade can't work as there's no character stats");
				return false;
			}

			characterStats.characterStats.fireRate.value = fireRate;
			return true;
		}
	}
}
