using UnityEngine;

namespace ProjectCHAOS.Worlds
{
	public class LevelArea : MonoBehaviour
	{
		private Collider _collider = null;

		private new Collider collider => this.GetCachedComponent(ref _collider);

		public Vector3 GetRandomPointXZ()
		{
			Vector3 min = collider.bounds.min;
			Vector3 max = collider.bounds.max;

			return new Vector3(
				Random.Range(min.x, max.x), 0f,
				Random.Range(min.z, max.z));
		}
	}
}
