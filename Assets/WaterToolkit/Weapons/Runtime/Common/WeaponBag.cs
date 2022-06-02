using WaterToolkit.ValueBoards;

namespace WaterToolkit.Weapons
{
	public class WeaponBag : ValueBoard<WeaponObject>
	{
		public WeaponBag() : base()
		{
			maxEntryCount = 8;
		}
	}
}
