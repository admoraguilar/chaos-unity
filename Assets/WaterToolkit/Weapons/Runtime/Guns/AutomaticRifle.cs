using UnityEngine;
using WaterToolkit.Behave;

namespace WaterToolkit.Weapons
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

		private void OnFireRateElapsed()
		{
			if(_isFiring) {
				Transform target = Targetting.GetNearestTransform(
					transform.position, 30f, 
					layerMask.value, transform);
				
				if(target != null) {
					Vector3 bulletLookDirection = Targetting.CalculateDirectionToTarget(transform, target);
					Bullet bullet = Instantiate(
						bulletPrefab, muzzlePoint.position,
						Quaternion.LookRotation(bulletLookDirection));
					BulletLaunchInfo bulletLaunchInfo = new BulletLaunchInfo {
						owner = transform,
						targetTransform = target,
						direction = transform.forward
					};

					bullet.Launch(bulletLaunchInfo);

					SendFireEvent(new WeaponFireInfo {
						visual = this,
						bullet = bullet
					});
				}
			}
		}

		private void Start()
		{
			_fireRate.Run();
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
