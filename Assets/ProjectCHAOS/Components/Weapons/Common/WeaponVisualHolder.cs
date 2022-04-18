using System;
using UnityEngine;

using UObject = UnityEngine.Object;

namespace ProjectCHAOS.Weapons
{
	public class WeaponVisualHolder
	{
		public event Action OnFire = delegate { };

		[SerializeField]
		private float _fireRateMultiplier = 1f;

		private Transform _parent = null;
		private WeaponVisual _visual = null;

		public float fireRateMultiplier
		{
			get => _fireRateMultiplier;
			set {
				_fireRateMultiplier = Mathf.Clamp(value, 1f, 5f);
				if(visual != null) {
					if(visual is AutomaticRifle rifle) {
						rifle.fireRate.speedMultiplier = fireRateMultiplier;
					}
				}
			}
		}

		public WeaponVisual visual
		{
			get => _visual;
			private set => _visual = value;
		}

		public Transform parent
		{
			get => _parent;
			set => _parent = value;
		}

		public void SetVisual(WeaponObject obj)
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
				visualPrefab, _parent, 
				false);
			if(visual is AutomaticRifle rifle) {
				rifle.fireRate.speedMultiplier = fireRateMultiplier;
			}
			visual.OnFire += InvokeOnFire;
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

		private void InvokeOnFire() => OnFire();
	}
}
