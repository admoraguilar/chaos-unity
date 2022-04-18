using System;
using UnityEngine;

namespace ProjectCHAOS.Weapons
{
	public class WeaponCycler
	{
		public event Action<WeaponObject> OnSetWeapon = delegate { };

		private int _index = -1;

		private WeaponBag _bag = null;

		public WeaponCycler(WeaponBag bag)
		{
			Initialize(bag);
		}

		public int index
		{
			get => _index;
			private set => _index = value;
		}

		public WeaponBag bag
		{
			get => _bag;
			private set => _bag = value;
		}

		public void Initialize(WeaponBag bag)
		{
			this.bag = bag;
		}

		public WeaponObject SetWeapon(int index)
		{
			if(_index != index) {
				WeaponObject result = bag[index];
				_index = Mathf.Clamp(index, 0, _bag.maxEntryCount);
				OnSetWeapon(result);
				return result;
			} else {
				_index = -1;
				OnSetWeapon(null);
				return null;
			}
		}
	}
}
