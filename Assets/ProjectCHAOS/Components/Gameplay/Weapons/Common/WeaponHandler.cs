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
		private WeaponDatabaseBuilder _databaseBuilder = null;

		[SerializeField]
		private Transform _visualHolderTransform = null;

		private WeaponDatabase _database = null;
		private WeaponVisualHolder _visualHolder = null;
		private WeaponBag _bag = null;
		private WeaponCycler _cycler = null;

		public WeaponDatabase database
		{
			get => _database;
			private set => _database = value;
		}

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

		private void OnSetWeapon(WeaponObject weaponObject)
		{
			visualHolder.SetVisual(weaponObject);
		}

		private void Awake()
		{
			database = _databaseBuilder.Build();
			visualHolder = new WeaponVisualHolder();
			bag = new WeaponBag();
			cycler = new WeaponCycler(bag);

			// Hack get all valid weapons in database
			bag.AddRange(database.Get(w => w.IsValid()));

			visualHolder.parent = _visualHolderTransform;
			visualHolder.SetVisual(cycler.SetWeapon(0));
		}

		private void OnEnable()
		{
			visualHolder.visual.OnFire += InvokeOnFire;
			cycler.OnSetWeapon += OnSetWeapon;
		}

		private void OnDisable()
		{
			visualHolder.visual.OnFire -= InvokeOnFire;
			cycler.OnSetWeapon -= OnSetWeapon;
		}

		private void Update()
		{
			if(Input.GetKeyDown(KeyCode.Alpha1)) {
				cycler.SetWeapon(0);
			}

			if(Input.GetKeyDown(KeyCode.Alpha2)) {
				cycler.SetWeapon(1);
			}

			if(Input.GetKeyDown(KeyCode.Alpha3)) {
				cycler.SetWeapon(2);
			}
		}
	}
}
