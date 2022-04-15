using ProjectCHAOS.Systems;

namespace ProjectCHAOS.Gameplay.Weapons
{
	public class WeaponBag : ValueBoard<WeaponObject>
	{
		public WeaponBag() : base()
		{
			maxEntryCount = 8;
		}
	}
}
