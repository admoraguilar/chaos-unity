using UnityEngine;

namespace WaterToolkit.Pickups
{
	public class Pickup : MonoBehaviour
	{
		[SerializeField]
		private PickupSpec _object = null;

		public PickupSpec @object => _object;
	}
}
