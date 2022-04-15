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
	///		* cycling
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

		[SerializeField]
		private WeaponVisual _visual = null;

		private WeaponBag _bag = new WeaponBag();

		public WeaponVisual visual => _visual;

		public WeaponBag bag => _bag;

		private void InvokeOnFire() => OnFire();

		private void OnEnable()
		{
			visual.OnFire += InvokeOnFire;
		}

		private void OnDisable()
		{
			visual.OnFire -= InvokeOnFire;
		}
	}
}
