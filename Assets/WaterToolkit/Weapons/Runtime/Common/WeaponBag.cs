using WaterToolkit.GameDatabases;

namespace WaterToolkit.Weapons
{
	public class WeaponBag : GameDatabase<WeaponObject>
	{
		public WeaponBag() : base()
		{
			maxEntryCount = 8;
		}
	}
}
