using ProjectCHAOS.ValueBoards;

namespace ProjectCHAOS.Weapons
{
	public class WeaponBag : ValueBoard<WeaponObject>
	{
		public WeaponBag() : base()
		{
			maxEntryCount = 8;
		}
	}
}
