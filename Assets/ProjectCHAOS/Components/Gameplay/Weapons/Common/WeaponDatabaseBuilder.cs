using System;
using UnityEngine;
using ProjectCHAOS.Systems;

namespace ProjectCHAOS.Gameplay.Weapons
{
	[CreateAssetMenu(menuName = "ProjectCHAOS/Weapon Database Builder")]
	public class WeaponDatabaseBuilder : 
		ValueBoardBuilder<
			WeaponDatabase, WeaponObject,
			WeaponDatabaseBuilder.WeaponDatabaseObjectBuilder>
	{
		[Serializable]
		public class WeaponDatabaseObjectBuilder : ValueObjectBuilder<WeaponObject>
		{
			public string name = string.Empty;
			public int magSize = 30;
			public int currentMagAmmountCount = 30;

			public override WeaponObject Build()
			{
				WeaponObject weapon = new WeaponObject();
				weapon.name = name;
				weapon.magSize = magSize;
				weapon.currentMagAmmoCount = currentMagAmmountCount;
				return weapon;
			}
		}
	}
}
