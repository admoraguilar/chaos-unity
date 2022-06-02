using System;
using System.Collections.Generic;
using UnityEngine;

namespace WaterToolkit.Upgrades
{
	public class Upgrader : MonoBehaviour
	{
		public event Action<int> OnObjectIndexChanged = delegate { };

		[SerializeField]
		private List<UpgradeObject> _objects = new List<UpgradeObject>();

		private List<Transform> _upgradableList = new List<Transform>();
		private int _objectIndex = -1;

		public IReadOnlyList<UpgradeObject> objects => _objects;

		public IReadOnlyList<Transform> upgradableList => _upgradableList;

		public int objectIndex
		{
			get => _objectIndex;
			set {
				// Loop object index on objects index
				if(value >= _objects.Count) {
					value = 0;
				} else if(value < -1){
					value = objects.Count - 1;
				} else if(isAllReachedMax) {
					value = -1;
				}

				_objectIndex = value;

				Debug.Log(objectIndex);

				// Skip object indexes that reached max upgrade already
				if(_objectIndex > -1 && _objects[_objectIndex].isReachedMax) {
					objectIndex++;
					return;
				}

				OnObjectIndexChanged(value);
			}
		}

		public bool isAllReachedMax
		{
			get {
				foreach(UpgradeObject upgradeObject in _objects) {
					if(!upgradeObject.isReachedMax) {
						return false;
					}
				}

				return true;
			}
		}

		public void AddUpgradables(IEnumerable<Transform> upgradables)
		{
			foreach(Transform upgradable in upgradables) {
				AddUpgradable(upgradable);
			}
		}

		public void AddUpgradable(Transform upgradable)
		{
			if(_upgradableList.Contains(upgradable)) {
				return;
			}

			_upgradableList.Add(upgradable);
		}

		public void RemoveUpgradable(Transform upgradable)
		{
			_upgradableList.Remove(upgradable);
		}

		public void Upgrade(int forceObjectIndex = -2, int forceUpgradeIndex = -2)
		{
			Upgrade(_upgradableList, forceObjectIndex, forceUpgradeIndex);
		}

		public void Upgrade(IEnumerable<Transform> upgradables, int forceObjectIndex = -2, int forceUpgradeIndex = -2)
		{
			foreach(Transform upgradable in upgradables) {
				Upgrade(upgradable, forceObjectIndex, forceUpgradeIndex);
			}
		}

		public void Upgrade(Transform upgradable, int forceObjectIndex = -2, int forceUpgradeIndex = -2)
		{
			if(forceObjectIndex > -2) { objectIndex = forceObjectIndex; }
			if(objectIndex < 0) {
				Debug.Log("No selected object index");
				return; 
			}

			_objects[objectIndex].Upgrade(upgradable, forceUpgradeIndex);
		}

		public void ResetAll()
		{
			ResetAll(_upgradableList);
		}

		public void ResetAll(IEnumerable<Transform> upgradables)
		{
			foreach(Transform upgradable in upgradables) {
				ResetAll(upgradable);
			}
		}

		public void ResetAll(Transform upgradable)
		{
			foreach(UpgradeObject @object in _objects) {
				@object.Reset(upgradable);
			}
		}

		public void ResetObjectIndex()
		{
			objectIndex = -1;
		}

		private void Update()
		{
			// Hack
			if(Input.GetKeyDown(KeyCode.A)) {
				objectIndex++;
			}
		}
	}
}
