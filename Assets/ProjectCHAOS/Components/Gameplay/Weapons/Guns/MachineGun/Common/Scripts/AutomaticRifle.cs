using UnityEngine;
using ProjectCHAOS.Gameplay.Behave;

namespace ProjectCHAOS.Gameplay.Weapons
{
	/// <summary>
	/// In the future, remove homing features here.
	/// </summary>
    public class AutomaticRifle : WeaponVisual
    {
		public Bullet bulletPrefab = null;
		public Transform muzzlePoint = null;
		public LayerMask layerMask = default;

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

		private Transform GetTarget()
		{
			Collider[] results = Physics.OverlapSphere(transform.position, 30f, layerMask.value);
			Transform target = null;

			if(results.Length > 0) {
				target = results[0].transform;
			}

			foreach(Collider result in results) {
				if(result.transform == transform) {
					continue;
				}

				Transform resultTransform = result.transform;
				float targetDistance = Vector3.Distance(transform.position, target.position);
				float resultDistance = Vector3.Distance(transform.position, resultTransform.position);
				if(resultDistance < targetDistance) {
					target = resultTransform;
				}
			}

			return target;
		}

		private Vector3 CalculateDirectionToTarget(Transform from, Transform to)
		{
			Vector3 result = from.position - to.position;
			return result.normalized;
		}

		private void OnFireRateElapsed()
		{
			if(_isFiring) {
				Transform target = GetTarget();
				if(target != null) {
					Vector3 direction = CalculateDirectionToTarget(transform, target);
					Bullet bullet = Instantiate(
						bulletPrefab, muzzlePoint.position,
						Quaternion.LookRotation(direction));
					bullet.Launch(transform, target, transform.forward);
					InvokeOnFire();
				}
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
