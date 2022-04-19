using UnityEngine;

namespace ProjectCHAOS.Pickups
{
	public class Pickup : MonoBehaviour
	{
		[SerializeField]
		private PickupObject _object = null;

		public PickupObject @object => _object;
	}
}
