using System.Collections.Generic;
using ProjectCHAOS.Systems;

namespace ProjectCHAOS.Gameplay.Weapons
{
	public class WeaponObject : ValueObject
	{
		public const string nameKey = "name";
		public const string magSizeKey = "magSize";
		public const string currentMagAmmoCountKey = "currentMagAmmoCount";

		public string name
		{
			get => GetValue<string>(nameKey);
			set => SetValue(nameKey, value);
		}

		public int magSize
		{
			get => GetValue<int>(magSizeKey);
			set => SetValue(magSizeKey, value);
		}

		public int currentMagAmmoCount
		{
			get => GetValue<int>(currentMagAmmoCountKey);
			set => SetValue(currentMagAmmoCountKey, value);
		}

		public WeaponObject(string key) : base(key)
		{
			name = string.Empty;
			magSize = 30;
			currentMagAmmoCount = magSize;
		}

		public WeaponObject(string key, IDictionary<string, object> values) : base(key, values)
		{
			name = string.Empty;
			magSize = 30;
			currentMagAmmoCount = magSize;
		}

		public override bool IsValid()
		{
			return base.IsValid() &&
				name != string.Empty && magSize >= 0 &&
				currentMagAmmoCount >= 0;
		}
	}
}
