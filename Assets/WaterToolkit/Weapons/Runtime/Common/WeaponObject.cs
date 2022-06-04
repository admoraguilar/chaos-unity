using System.Collections.Generic;
using WaterToolkit.GameDatabases;

namespace WaterToolkit.Weapons
{
	public class WeaponObject : GameDatabaseObject
	{
		public const string nameKey = "name";
		public const string prefabKey = "prefabKey";
		public const string magSizeKey = "magSize";
		public const string currentMagAmmoCountKey = "currentMagAmmoCount";

		public string name
		{
			get => GetValue<string>(nameKey);
			set => SetValue(nameKey, value);
		}

		public WeaponVisual prefab
		{
			get => GetValue<WeaponVisual>(prefabKey);
			set => SetValue(prefabKey, value);
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

		public WeaponObject() : base() { }

		public WeaponObject(string key) : base(key) { }

		public WeaponObject(string key, IDictionary<string, object> values) : base(key, values) { }

		protected override void Initialize(string key = null, IDictionary<string, object> values = null)
		{
			base.Initialize(key, values);

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
