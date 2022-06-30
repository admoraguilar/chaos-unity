using System;
using UnityEngine;

namespace WaterToolkit.Weapons
{
	public abstract class WeaponVisual : MonoBehaviour
	{
		public event Action<WeaponFireInfo> OnFire = delegate { };

		private WeaponVisualHolder _holder = null;

		private Transform _transform = null;

		public WeaponVisualHolder holder
		{
			get => _holder;
			internal set => _holder = value;
		}
		
		public new Transform transform => this.GetCachedComponent(ref _transform);

		public abstract void StartFiring();
		public abstract void StopFiring();

		internal virtual void OnStatsChange() { }

		protected void SendFireEvent(Bullet bullet)
		{
			OnFire(new WeaponFireInfo {
				visual = this,
				bullet = bullet
			});
		}
	}
}
