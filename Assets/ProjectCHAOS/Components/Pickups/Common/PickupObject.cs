using UnityEngine;

namespace ProjectCHAOS.Pickups
{
	public abstract class PickupObject : ScriptableObject
	{
		public abstract void Initialize(Transform reference);

		public abstract void Pickup(Transform owner);
	}
}
