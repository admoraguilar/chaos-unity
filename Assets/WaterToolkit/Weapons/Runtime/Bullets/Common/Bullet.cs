using System;
using UnityEngine;

namespace WaterToolkit.Weapons
{
	public abstract class Bullet : MonoBehaviour
	{
		public event Action<BulletLifeInfo> OnEndLifetime = delegate { };

		public float speed = 50f;
		public float lifetime = 3f;
		public LayerMask layerMask = default;

		private BulletLaunchInfo _bulletLaunchInfo = default;

		private Transform _transform = null;

		public new Transform transform => this.GetCachedComponent(ref _transform);

		public BulletLaunchInfo launchInfo
		{
			get => _bulletLaunchInfo;
			private set => _bulletLaunchInfo = value;
		}

		public void Launch(BulletLaunchInfo launchInfo)
		{
			this.launchInfo = launchInfo;
			OnLaunch();
		}

		protected abstract void OnLaunch();

		protected void SendEndLifetimeEvent(BulletHitInfo hitInfo)
		{
			OnEndLifetime(new BulletLifeInfo {
				bullet = this,
				launch = launchInfo,
				hit = hitInfo
			});
		}
	}
}