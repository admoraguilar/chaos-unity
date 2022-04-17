using UnityEngine;

namespace ProjectCHAOS.Gameplay.Weapons
{
	public class SimpleBullet : Bullet
	{
		private Vector3 _direction = Vector3.zero;
		private bool _isTravelling = false;

		public override void Launch(Vector3 direction)
		{
			_direction = direction;
			_isTravelling = true;

			transform.rotation = Quaternion.LookRotation(_direction, Vector3.up);
			Destroy(gameObject, lifetime);
		}

		private void FixedUpdate()
		{
			if(_isTravelling) {
				transform.Translate(Vector3.forward * speed * Time.deltaTime, Space.Self);
			}
		}
	}
}
