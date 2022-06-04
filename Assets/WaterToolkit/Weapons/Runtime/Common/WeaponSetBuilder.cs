using System;
using UnityEngine;
using WaterToolkit.GameDatabases;

namespace WaterToolkit.Weapons
{
	[CreateAssetMenu(menuName = "WaterToolkit/Weapons/Weapon Database Builder")]
	public class WeaponSetBuilder : 
		GameDatabaseBuilder<
			WeaponSet, WeaponObject,
			WeaponSetBuilder.WeaponSetObjectBuilder>
	{
		[Serializable]
		public class WeaponSetObjectBuilder : ValueObjectBuilder<WeaponObject>
		{
			public string name = string.Empty;
			public WeaponVisual prefab = null;
			public int magSize = 30;
			public int currentMagAmmountCount = 30;

			public override WeaponObject Build()
			{
				WeaponObject weapon = new WeaponObject();
				weapon.name = name;
				weapon.prefab = prefab;
				weapon.magSize = magSize;
				weapon.currentMagAmmoCount = currentMagAmmountCount;
				return weapon;
			}
		}
	}
}
