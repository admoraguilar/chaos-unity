using System.Collections.Generic;
using UnityEngine;
using WaterToolkit.Behave;

namespace WaterToolkit.Pickups
{
	public class PickupHandler : MonoBehaviour
	{
		[SerializeField]
		private FlyweightSpec<PickupSpec> _pickups = new FlyweightSpec<PickupSpec>();

		[SerializeField]
		private CollisionEvents _collisionEvents = null;
		
		public void AddReferences(IEnumerable<object> references)
		{
			_pickups.AddReferences(references);
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
			AddReferences(new object[] { this });
		}

		private void Start()
		{
			_pickups.Initialize();
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
