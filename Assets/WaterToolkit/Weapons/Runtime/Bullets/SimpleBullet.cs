using UnityEngine;

namespace WaterToolkit.Weapons
{
	public class SimpleBullet : Bullet
	{
		private bool _isTravelling = false;

		protected override void OnLaunch()
		{
			_isTravelling = true;

			transform.rotation = Quaternion.LookRotation(launchInfo.direction, Vector3.up);
			Destroy(gameObject, lifetime);
		}

		//private void OnDestroy()
		//{
		//	SendEndLifetimeEvent(new BulletHitInfo {
		//		position = transform.position,
		//		isSuccess = false
		//	});
		//}

		private void FixedUpdate()
		{
			if(_isTravelling) {
				transform.Translate(Vector3.forward * speed * Time.deltaTime, Space.Self);
			}
		}

		private void OnTriggerEnter(Collider other)
		{
			if(!layer.Includes(other.gameObject.layer)) { return; }

			SendEndLifetimeEvent(new BulletHitInfo {
				position = other.transform.position,
				isSuccess = true
			});

			Destroy(gameObject);
		}
	}
}
