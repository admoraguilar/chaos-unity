using UnityEngine;
using WaterToolkit;

namespace WaterToolkit.Weapons
{
	public abstract class Bullet : MonoBehaviour
	{
		public float speed = 50f;
		public float lifetime = 3f;

		private Transform _transform = null;

		public new Transform transform => this.GetCachedComponent(ref _transform);

		public abstract bool Launch(Transform owner, Transform target, Vector3 direction);
	}
}