using UnityEngine;
using WaterToolkit.GameDatabases;

namespace WaterToolkit.Weapons
{
	[CreateAssetMenu(menuName = "WaterToolkit/Weapons/Weapon Bag")]
	public class WeaponBag : GameCollection<WeaponItem>
	{
		protected override void OnEnable()
		{
			base.OnEnable();

			maxEntryCount = 8;
		}
	}
}
