using UnityEngine;
using ProjectCHAOS.Systems;

namespace ProjectCHAOS.Gameplay.Spawners
{
	public class Killzone : MonoBehaviour
	{
		public LayerMask layerMask;

		private void OnTriggerEnter(Collider other)
		{
			GameObject go = other.gameObject;
			if(layerMask.Includes(go.layer)) {
				Destroy(go);
			}
		}
	}
}
