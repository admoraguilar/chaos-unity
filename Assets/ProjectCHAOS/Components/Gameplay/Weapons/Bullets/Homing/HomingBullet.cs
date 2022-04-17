using UnityEngine;

namespace ProjectCHAOS.Gameplay.Weapons
{
	public class HomingBullet : Bullet
	{
		public LayerMask layerMask = default;

		private Vector3 _direction = Vector3.zero;
		private bool _isTravelling = false;

		private Transform _target = null;
		private Vector3 _lastDirectionToTarget = Vector3.zero;
		private Vector3 _lastTargetPosition = Vector3.zero;

		public override void Launch(Vector3 direction)
		{
			_direction = direction;
			_isTravelling = true;

			Collider[] results = Physics.OverlapSphere(transform.position, 30f, layerMask.value);
			_target = null;

			if(results.Length > 0) {
				_target = results[0].transform;
			}
 
			foreach(Collider result in results) {
				if(result.transform == transform) {
					continue;
				}

				Transform resultTransform = result.transform;
				float targetDistance = Vector3.Distance(transform.position, _target.position);
				float resultDistance = Vector3.Distance(transform.position, resultTransform.position);
				if(resultDistance < targetDistance) {
					_target = resultTransform;
				}
			}

			if(_target == null) {
				Destroy(gameObject);
			} else {
				Destroy(gameObject, lifetime);
			}
		}

		private void FixedUpdate()
		{
			if(_isTravelling && _target != null) {
				_lastDirectionToTarget = transform.position - _target.position;
				_lastTargetPosition = _target.position;
				transform.rotation = Quaternion.LookRotation(_lastDirectionToTarget.normalized, Vector3.up);
				transform.position = Vector3.MoveTowards(transform.position, _lastTargetPosition, speed * Time.deltaTime);
			}

			if(_isTravelling && _target == null) {
				transform.position += -transform.forward * speed * Time.deltaTime;
			}
		}
	}
}
