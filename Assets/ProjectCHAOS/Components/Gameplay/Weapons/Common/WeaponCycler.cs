using System;
using UnityEngine;

namespace ProjectCHAOS.Gameplay.Weapons
{
	public class WeaponCycler
	{
		public event Action<WeaponObject> OnSetWeapon = delegate { };

		private int _index = 0;

		private WeaponBag _bag = null;

		public WeaponCycler(WeaponBag bag)
		{
			_bag = bag;
		}

		public int index
		{
			get => _index;
			private set => _index = value;
		}

		public WeaponBag bag => _bag;

		public WeaponObject SetWeapon(int index)
		{
			WeaponObject result = bag[index];
			if(_index != index) {
				_index = Mathf.Clamp(index, 0, _bag.maxEntryCount);
				OnSetWeapon(result);
			}

			return result;
		}
	}
}
