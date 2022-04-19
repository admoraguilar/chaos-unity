using System.Collections.Generic;
using UnityEngine;

namespace ProjectCHAOS.Upgrades
{
	public class Upgrader : MonoBehaviour
	{
		[SerializeField]
		private List<UpgradeObject> _objects = new List<UpgradeObject>();

		private int _objectIndex = -1;

		public IReadOnlyList<UpgradeObject> objects => _objects;

		public int objectIndex
		{
			get => _objectIndex;
			private set {
				_objectIndex = Mathf.Clamp(value, -1, _objects.Count);
			}
		}

		public void Upgrade(Transform transform, int forceObjectIndex = -2, int forceUpgradeIndex = -2)
		{
			if(forceObjectIndex < -1) { objectIndex++; } 
			else { objectIndex = forceObjectIndex; }

			_objects[objectIndex].Upgrade(transform, forceUpgradeIndex);
		}
	}
}
