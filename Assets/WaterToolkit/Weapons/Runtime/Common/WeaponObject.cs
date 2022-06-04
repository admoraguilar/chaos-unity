using System;

namespace WaterToolkit.Weapons
{
	[Serializable]
	public class WeaponObject
	{
		public string name = string.Empty;
		public WeaponVisual prefab = null;
		public int magSize = 0;
		public int currentMagAmmoCount = 0;

		public bool IsValid()
		{
			return name != string.Empty && magSize >= 0 &&
				   currentMagAmmoCount >= 0;
		}
	}
}
