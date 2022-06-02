using UnityEngine;

namespace WaterToolkit.Weapons
{
	public class SimpleBullet : Bullet
	{
		private Vector3 _direction = Vector3.zero;
		private bool _isTravelling = false;

		public override bool Launch(Transform owner, Transform target, Vector3 direction)
		{
			_direction = direction;
			_isTravelling = true;

			transform.rotation = Quaternion.LookRotation(_direction, Vector3.up);
			Destroy(gameObject, lifetime);

			return true;
		}

		private void FixedUpdate()
		{
			if(_isTravelling) {
				transform.Translate(Vector3.forward * speed * Time.deltaTime, Space.Self);
			}
		}
	}
}
