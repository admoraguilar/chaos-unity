using UnityEngine;

namespace ProjectCHAOS
{
	public class BasicAI : MonoBehaviour
	{
		public Vector3 direction = Vector3.zero;
		public float speed = 10f;

		private Transform _transform = null;

		public new Transform transform => this.GetCachedComponent(ref _transform);

		private void FixedUpdate()
		{
			transform.Translate(direction * speed * Time.deltaTime, Space.Self);
		}

		private void OnCollisionEnter(Collision collision)
		{
			if(collision.gameObject.TryGetComponent(out Bullet bullet)) {
				Destroy(gameObject);
				Destroy(bullet.gameObject);
			}
		}
	}
}