using UnityEngine;
using ProjectCHAOS.Systems;

namespace ProjectCHAOS.Gameplay.Weapons
{
	public abstract class Bullet : MonoBehaviour
	{
		public float speed = 50f;
		public float lifetime = 3f;

		private Transform _transform = null;

		public new Transform transform => this.GetCachedComponent(ref _transform);

		public abstract void Launch(Vector3 direction);
	}
}