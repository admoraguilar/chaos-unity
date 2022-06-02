using System.Collections.Generic;
using UnityEngine;
using WaterToolkit.Behave;

namespace WaterToolkit.Pickups
{
	public class PickupHandler : MonoBehaviour
	{
		[SerializeField]
		private List<PickupObject> _objectsList = new List<PickupObject>();

		[SerializeField]
		private CollisionEvents _collisionEvents = null;

		private Dictionary<int, PickupObject> _objectsDict = new Dictionary<int, PickupObject>();
		private List<Transform> _referencesList = new List<Transform>();

		private Transform _transform = null;

		public new Transform transform => _transform;

		public void Initialize(IEnumerable<Transform> references)
		{
			foreach(PickupObject obj in _objectsList) {
				_objectsDict[obj.GetInstanceID()] = Instantiate(obj);
			}

			AddReferences(references);
		}

		public void AddReferences(IEnumerable<Transform> references)
		{
			foreach(Transform reference in references) {
				AddReference(reference);
			}
		}

		public void AddReference(Transform reference)
		{
			if(_referencesList.Contains(reference)) { return; }
			_referencesList.Add(reference);

			foreach(PickupObject obj in _objectsDict.Values) {
				obj.Initialize(reference);
			}
		}

		public void RemoveReference(Transform reference)
		{
			_referencesList.Remove(reference);
		}

		private void OnTriggerEnterResponse(Collider collider)
		{
			Pickup pickup = collider.GetComponent<Pickup>();
			if(pickup != null) {
				int key = pickup.@object.GetInstanceID();
				PickupObject obj = _objectsDict[key];
				obj.Pickup(transform);

				Destroy(pickup.gameObject);
			}
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
