using UnityEngine;
using ProjectCHAOS.Common;

namespace ProjectCHAOS.Weapons
{
	public class Bullet : MonoBehaviour
	{
		public float speed = 10f;
		public float lifetime = 3f;

		private Vector3 _direction = Vector3.zero;
		private bool _isTravelling = false;

		private Transform _transform = null;

		public new Transform transform => this.GetCachedComponent(ref _transform);

		public void Launch(Vector3 direction)
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