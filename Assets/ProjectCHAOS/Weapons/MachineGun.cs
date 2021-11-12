using UnityEngine;

namespace ProjectCHAOS
{
    public class MachineGun : MonoBehaviour
    {
		public Bullet bulletPrefab = null;
		public Transform muzzlePoint = null;
		public float fireRate = .1f;

		private bool _isFiring = false;
		private float _fireTimer = 0f;

		public void StartFiring()
		{
			_isFiring = true;
			_fireTimer = fireRate;
		}

		public void StopFiring()
		{
			_isFiring = false;
			_fireTimer = 0f;
		}

		private void FixedUpdate()
		{
			if(_isFiring) {
				_fireTimer += Time.deltaTime;
				if(_fireTimer > fireRate) {
					Bullet bullet = Instantiate(
						bulletPrefab, muzzlePoint.position,
						Quaternion.identity);
					bullet.Launch(transform.forward);

					_fireTimer = 0f;
				}
			}
		}
	}
}
