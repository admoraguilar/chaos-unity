using System.Collections.Generic;
using UnityEngine;

namespace ProjectCHAOS.Gameplay.Interactions
{
	public class PickupAbility : MonoBehaviour
	{
		public List<PickupResponse> responses = new List<PickupResponse>();

		private void OnTriggerEnter2D(Collider2D collider)
		{
			IPickup pickup = collider.GetComponent<IPickup>();
			if (pickup != null) {
				foreach(PickupResponse response in responses) {
					response.Respond(gameObject, pickup);
				}
				
				pickup.Pick(); 
			}
		}
	}
}
