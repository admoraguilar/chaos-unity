using System;
using UnityEngine;

namespace ProjectCHAOS.Interactions
{
	public abstract class PickupResponse : ScriptableObject 
	{
		public abstract Type pickupType { get; }

		public void Respond(GameObject owner, IPickup pickup)
		{
			if(!pickupType.IsSubclassOf(pickup.GetType())) { return; }
			OnPickup(owner, pickup);
		}

		protected abstract void OnPickup(GameObject owner, IPickup pickup);
	}
}
