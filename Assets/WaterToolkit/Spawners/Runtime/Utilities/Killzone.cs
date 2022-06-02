using UnityEngine;
using WaterToolkit;
using WaterToolkit.Behave;

namespace WaterToolkit.Spawners
{
	public class Killzone : MonoBehaviour
	{
		public LayerMask layerMask;

		private void OnTriggerEnter(Collider other)
		{
			GameObject go = other.gameObject;
			if(layerMask.Includes(go.layer)) {
				IHealth health = go.GetComponentInParent<IHealth>();
				if(health != null) {
					health.health.Kill();
				} else {
					Destroy(go);
				}	
			}
		}
	}
}
