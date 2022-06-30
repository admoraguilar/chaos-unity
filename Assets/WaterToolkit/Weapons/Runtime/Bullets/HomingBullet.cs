using UnityEngine;
using WaterToolkit.Behave;

namespace WaterToolkit.Weapons
{
	public class HomingBullet : Bullet
	{
		private bool _isTravelling = false;
		private bool _isHitSuccess = false;

		private Vector3 _lastDirectionToTarget = Vector3.zero;
		private Vector3 _lastTargetPosition = Vector3.zero;

		protected override void OnLaunch()
		{
			_isTravelling = true;
			Destroy(gameObject, lifetime);
		}

		private void OnDestroy()
		{
			SendEndLifetimeEvent(new BulletHitInfo {
				position = transform.position,
				isSuccess = _isHitSuccess
			});
		}

		private void FixedUpdate()
		{
			if(_isTravelling && launchInfo.targetTransform != null) {
				_lastDirectionToTarget = Targetting.CalculateDirectionToTarget(transform, launchInfo.targetTransform);
				_lastTargetPosition = launchInfo.targetTransform.position;
				transform.rotation = Quaternion.LookRotation(_lastDirectionToTarget, Vector3.up);
				transform.position = Vector3.MoveTowards(transform.position, _lastTargetPosition, speed * Time.deltaTime);
			}

			if(_isTravelling && launchInfo.targetTransform == null) {
				transform.position += speed * Time.deltaTime * -transform.forward;
			}
		}

		private void OnTriggerEnter(Collider other)
		{
			if(!layerMask.Includes(other.gameObject.layer)) { return; }

			_isHitSuccess = true;
			Destroy(gameObject);
		}
	}
}
