using UnityEngine;
using ProjectCHAOS.Utilities;

namespace ProjectCHAOS.Rules
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
