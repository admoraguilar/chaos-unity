using System;
using UnityEngine;

using UObject = UnityEngine.Object;

namespace ProjectCHAOS.Gameplay.Weapons
{
	public class WeaponVisualHolder
	{
		public event Action OnFire = delegate { };

		private Transform _parent = null;
		private WeaponVisual _visual = null;

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
