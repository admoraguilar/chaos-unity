using System;
using UnityEngine;
using ProjectCHAOS.Systems;

namespace ProjectCHAOS.Gameplay.Weapons
{
	public abstract class WeaponVisual : MonoBehaviour
	{
		public event Action OnFire = delegate { };

		private Transform _transform = null;

		public new Transform transform => this.GetCachedComponent(ref _transform);

		public abstract void StartFiring();
		public abstract void StopFiring();

		protected void InvokeOnFire() => OnFire();
	}
}
