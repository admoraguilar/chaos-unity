using System;
using UnityEngine;

namespace ProjectCHAOS.Gameplay.Weapons
{
    public class MachineGun : MonoBehaviour
    {
		public event Action OnFire = delegate { };

		public Bullet bulletPrefab = null;
		public Transform muzzlePoint = null;
		public float fireRate = .1f;

		private bool _isFiring = false;
		private float _fireTimer = 0f;

		public void StartFiring()
		{
			_isFiring = true;
		}

		public void StopFiring()
		{
			_isFiring = false;
		}

		private void FixedUpdate()
		{
			bool wasFiring = _fireTimer > 0f;
			bool firstFireAfterFireRateElased = _fireTimer <= 0f;
			bool fireRateElapsed = _fireTimer >= fireRate;

			if(_isFiring && (firstFireAfterFireRateElased || fireRateElapsed)) {
				Bullet bullet = Instantiate(
					bulletPrefab, muzzlePoint.position,
					Quaternion.identity);
				bullet.Launch(transform.forward);
			}

			if(_isFiring || wasFiring) {
				if(fireRateElapsed) {
					_fireTimer = 0f;
					OnFire();
				}

				_fireTimer += Time.deltaTime;
			}
		}
	}
}
