using System.Collections.Generic;
using UnityEngine;
using WaterToolkit.Behave;

namespace WaterToolkit.Pickups
{
	public class PickupHandler : MonoBehaviour
	{
		[SerializeField]
		private FlyweightContainer<PickupSpec> _pickups = new FlyweightContainer<PickupSpec>();

		[SerializeField]
		private CollisionEvents _collisionEvents = null;

		public void Initialize(IEnumerable<object> references)
		{
			foreach(PickupSpec pickup in _pickups) {
				pickup.Initialize(references);
			}
		}

		private void OnTriggerEnterResponse(Collider collider)
		{
			Pickup pickup = collider.GetComponent<Pickup>();
			if(pickup != null) {
				int key = pickup.@object.GetInstanceID();
				PickupSpec spec = _pickups.Find(p => p.source.GetInstanceID() == key);
				spec.Pickup();
				Destroy(pickup.gameObject);
			}
		}

		private void Awake()
		{
			Initialize(new object[] { this });
		}

		private void OnEnable()
		{
			_collisionEvents.OnTriggerEnterResponse += OnTriggerEnterResponse;
		}

		private void OnDisable()
		{
			_collisionEvents.OnTriggerEnterResponse -= OnTriggerEnterResponse;
		}
	}
}
