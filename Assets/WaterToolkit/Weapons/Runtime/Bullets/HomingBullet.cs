using UnityEngine;
using WaterToolkit.Behave;

namespace WaterToolkit.Weapons
{
	public class HomingBullet : Bullet
	{
		private Vector3 _direction = Vector3.zero;
		private bool _isTravelling = false;

		private Transform _target = null;
		private Vector3 _lastDirectionToTarget = Vector3.zero;
		private Vector3 _lastTargetPosition = Vector3.zero;

		public override bool Launch(Transform owner, Transform target, Vector3 direction)
		{
			_direction = direction;
			_isTravelling = true;
			_target = target;

			Destroy(gameObject, lifetime);
			return true;
		}

		private void FixedUpdate()
		{
			if(_isTravelling && _target != null) {
				_lastDirectionToTarget = Targetting.CalculateDirectionToTarget(transform, _target);
				_lastTargetPosition = _target.position;
				transform.rotation = Quaternion.LookRotation(_lastDirectionToTarget, Vector3.up);
				transform.position = Vector3.MoveTowards(transform.position, _lastTargetPosition, speed * Time.deltaTime);
			}

			if(_isTravelling && _target == null) {
				transform.position += -transform.forward * speed * Time.deltaTime;
			}
		}
	}
}
