using System;
using UnityEngine;

namespace WaterToolkit.Weapons
{
	public abstract class WeaponVisual : MonoBehaviour
	{
		public event Action<WeaponFireInfo> OnFire = delegate { };

		private Transform _transform = null;

		public new Transform transform => this.GetCachedComponent(ref _transform);

		public abstract void StartFiring();
		public abstract void StopFiring();

		protected void SendFireEvent(WeaponFireInfo fireInfo) => OnFire(fireInfo);
	}
}
