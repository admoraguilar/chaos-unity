using UnityEngine;

namespace ProjectCHAOS.Gameplay.Weapons
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

		private Vector3 CalculateDirectionToTarget(Transform from, Transform to)
		{
			Vector3 result = from.position - to.position;
			return result.normalized;
		}

		private void FixedUpdate()
		{
			if(_isTravelling && _target != null) {
				_lastDirectionToTarget = CalculateDirectionToTarget(transform, _target);
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
