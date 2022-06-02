using System;
using UnityEngine;

namespace WaterToolkit.Worlds
{
	public class Portal : MonoBehaviour
	{
		public event Action<GameObject> OnEnter = delegate { };

		public bool isAutoEnter = false;

		public void Enter(GameObject toEnter)
		{
			OnEnter(toEnter);
		}

		private void OnTriggerEnter(Collider other)
		{
			if(isAutoEnter) {
				Enter(other.gameObject);
			}
		}
	}
}
