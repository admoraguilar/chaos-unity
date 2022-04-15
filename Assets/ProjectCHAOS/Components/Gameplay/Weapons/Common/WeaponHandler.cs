using System;
using UnityEngine;

namespace ProjectCHAOS.Gameplay.Weapons
{
	public class WeaponData
	{
		public string id;
		public string name;
		public int ammoCount;
		public int magSize;
	}

	/// <summary>
	/// Central behaviour for all things weapons.
	/// Handles weapon: (could be separate classes though)
	///		* v cycler
	///		* equipment
	///		* pickup
	///		* upgrades (individual or all gun types)
	///		* buffs
	///		* database
	///		* v bag
	///		* v object
	///		* inventory connection
	///		* v visual
	/// </summary>
	public class WeaponHandler : MonoBehaviour
	{
		public event Action OnFire = delegate { };

		private WeaponVisualHolder _visualHolder = null;
		private WeaponBag _bag = null;
		private WeaponCycler _cycler = null;

		public WeaponVisualHolder visualHolder
		{
			get => _visualHolder;
			private set => _visualHolder = value;
		}

		public WeaponBag bag
		{
			get => _bag;
			private set => _bag = value;
		}

		public WeaponCycler cycler
		{
			get => _cycler;
			private set => _cycler = value;
		}

		private void InvokeOnFire() => OnFire();

		private void Awake()
		{
			bag = new WeaponBag();
			cycler = new WeaponCycler(bag);
		}

		private void OnEnable()
		{
			
		}

		private void OnDisable()
		{
			
		}
	}
}
