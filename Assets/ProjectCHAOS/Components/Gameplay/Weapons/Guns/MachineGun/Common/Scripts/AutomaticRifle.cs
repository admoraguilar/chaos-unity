using UnityEngine;
using ProjectCHAOS.Gameplay.Behave;

namespace ProjectCHAOS.Gameplay.Weapons
{
    public class AutomaticRifle : WeaponVisual
    {
		public Bullet bulletPrefab = null;
		public Transform muzzlePoint = null;

		[SerializeField]
		private Timing _fireRate = null;
		private bool _isFiring = false;

		public Timing fireRate => _fireRate;

		public override void StartFiring()
		{
			_isFiring = true;
		}

		public override void StopFiring()
		{
			_isFiring = false;
		}

		private void OnFireRateElapsed()
		{
			if(_isFiring) {
				Bullet bullet = Instantiate(
					bulletPrefab, muzzlePoint.position,
					Quaternion.identity);
				bullet.Launch(transform.forward);
				InvokeOnFire();
			}
		}

		private void OnEnable()
		{
			_fireRate.OnReachMax += OnFireRateElapsed;
		}

		private void OnDisable()
		{
			_fireRate.OnReachMax -= OnFireRateElapsed;
		}

		private void FixedUpdate()
		{
			_fireRate.Update();
		}
	}
}
