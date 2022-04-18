using ProjectCHAOS;

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
