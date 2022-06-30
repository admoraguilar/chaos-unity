using UnityEngine;

namespace WaterToolkit.Weapons
{
	public class SimpleBullet : Bullet
	{
		private bool _isTravelling = false;
		private bool _isHitSuccess = false;

		protected override void OnLaunch()
		{
			_isTravelling = true;

			transform.rotation = Quaternion.LookRotation(launchInfo.direction, Vector3.up);
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
			if(_isTravelling) {
				transform.Translate(Vector3.forward * speed * Time.deltaTime, Space.Self);
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
