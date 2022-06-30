using System.Collections.Generic;
using UnityEngine;
using WaterToolkit.Behave;

namespace WaterToolkit.Pickups
{
	public class PickupHandler : MonoBehaviour
	{
		[SerializeField]
		private Flyweight<PickupSpec> _pickups = new Flyweight<PickupSpec>();

		[SerializeField]
		private CollisionEvents _collisionEvents = null;

		private List<object> _references = new List<object>();

		private void OnPickupAdd(PickupSpec pickup)
		{
			pickup.Initialize(_references);
		}

		public void Initialize(IEnumerable<object> references)
		{
			_references.AddRange(references);
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

		private void Start()
		{
			_pickups.Initialize();
		}

		private void OnEnable()
		{
			_collisionEvents.OnTriggerEnterResponse += OnTriggerEnterResponse;
			_pickups.OnAdd += OnPickupAdd;
		}

		private void OnDisable()
		{
			_collisionEvents.OnTriggerEnterResponse -= OnTriggerEnterResponse;
			_pickups.OnAdd -= OnPickupAdd;
		}
	}
}
