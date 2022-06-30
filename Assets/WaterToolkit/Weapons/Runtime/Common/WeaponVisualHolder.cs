using System;
using UnityEngine;

using UObject = UnityEngine.Object;

namespace WaterToolkit.Weapons
{
	public class WeaponVisualHolder
	{
		public event Action<WeaponFireInfo> OnFire = delegate { };

		[SerializeField]
		private float _fireRateMultiplier = 1f;

		private Transform _container = null;
		private WeaponVisual _visual = null;

		public float fireRateMultiplier
		{
			get => _fireRateMultiplier;
			set {
				_fireRateMultiplier = Mathf.Clamp(value, 1f, 5f);
				if(visual != null) { visual.OnStatsChange(); }
			}
		}

		public WeaponVisual visual
		{
			get => _visual;
			private set => _visual = value;
		}

		public Transform container
		{
			get => _container;
			set => _container = value;
		}

		public void SetVisual(WeaponItem obj)
		{
			if(visual != null) {
				visual.OnFire -= InvokeOnFire;
				UObject.Destroy(visual.gameObject);
			}

			if(obj == null) {
				return;
			}

			WeaponVisual visualPrefab = obj.prefab;
			visual = UObject.Instantiate(
				visualPrefab, _container, 
				false);
			visual.holder = this;
			visual.OnFire += InvokeOnFire;

			visual.OnStatsChange();
		}

		public void StartFiring()
		{
			if(visual == null) { return; }
			visual.StartFiring();
		}

		public void StopFiring()
		{
			if(visual == null) { return; }
			visual.StopFiring();
		}

		private void InvokeOnFire(WeaponFireInfo fireInfo) => OnFire(fireInfo);
	}
}
