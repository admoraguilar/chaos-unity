using WaterToolkit.GameDatabases;

namespace WaterToolkit.Weapons
{
	public class WeaponBag : GameDatabaseCollection<WeaponObject>
	{
		public WeaponBag() : base()
		{
			maxEntryCount = 8;
		}
	}
}
