using System;
using UnityEngine;

namespace WaterToolkit.Weapons
{
	/// <summary>
	/// Central behaviour for all things weapons.
	/// Handles weapon: (could be separate classes though)
	///		* v cycler
	///		* equipment
	///		* pickup
	///		* upgrades (individual or all gun types)
	///		* buffs
	///		* v database
	///		* v bag
	///		* v object
	///		* inventory connection
	///		* v visual
	/// </summary>
	public class WeaponHandler : MonoBehaviour
	{
		public event Action OnFire = delegate { };

		[SerializeField]
		private WeaponSet _startingLoadout = null;

		[SerializeField]
		private bool _shouldEquipWeaponAtStart = false;

		[SerializeField]
		private Transform _visualHolderTransform = null;
		
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

		private void OnSetWeapon(WeaponItem weaponObject)
		{
			visualHolder.SetVisual(weaponObject);
		}

		private void Awake()
		{
			visualHolder = new WeaponVisualHolder();
			visualHolder.parent = _visualHolderTransform;

			bag = new WeaponBag();
			bag.AddRange(_startingLoadout.Get(w => w.IsValid())); // Hack get all valid weapons in database

			cycler = new WeaponCycler(bag);
		}

		private void OnEnable()
		{
			visualHolder.OnFire += InvokeOnFire;
			cycler.OnSetWeapon += OnSetWeapon;
		}

		private void OnDisable()
		{
			visualHolder.OnFire -= InvokeOnFire;
			cycler.OnSetWeapon -= OnSetWeapon;
		}

		private void Start()
		{
			if(_shouldEquipWeaponAtStart) {
				visualHolder.SetVisual(cycler.SetWeapon(0));
			}
		}
	}
}
