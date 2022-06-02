using System;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectCHAOS.Upgrades
{
	public abstract class UpgradeObject<T> : UpgradeObject
		where T : UpgradeBehaviour
	{
		[SerializeField]
		private List<T> _behaviours = null;

		public IReadOnlyList<T> behaviours => _behaviours;

		public override int maxIndex => behaviours.Count - 1;

		protected override void OnUpgradeMethod(Transform toUpgrade, int forceIndex = -2)
		{
			behaviours[forceIndex].Upgrade(toUpgrade);
		}
	}

	public abstract class UpgradeObject : ScriptableObject
	{
		public event Action<UpgradeObject, Transform, int> OnUpgrade = delegate { };

		public new string name = string.Empty;

		[NonSerialized]
		private int _index = 0;

		public bool isReachedMax => index >= maxIndex;

		public abstract int maxIndex { get; }

		public int index
		{
			get => _index;
			private set {
				_index = Mathf.Clamp(value, 0, maxIndex);
			}
		}

		public void Upgrade(IEnumerable<Transform> toUpgrades, int forceIndex = -2)
		{
			foreach(Transform toUpgrade in toUpgrades) {
				Upgrade(toUpgrade, forceIndex);
			}
		}

		public void Upgrade(Transform toUpgrade, int forceIndex = -2)
		{
			if(isReachedMax) {
				Debug.Log("Max index reached");
				return;
			}

			if(forceIndex < -1) { index++; } 
			else { index = forceIndex; }

			OnUpgradeMethod(toUpgrade, index);
			OnUpgrade(this, toUpgrade, index);
		}

		protected abstract void OnUpgradeMethod(Transform toUpgrade, int forceIndex = -2);

		public void Reset(IEnumerable<Transform> toUpgrades)
		{
			foreach(Transform toUpgrade in toUpgrades) {
				Reset(toUpgrade);
			}
		}

		public void Reset(Transform toUpgrade)
		{
			Upgrade(toUpgrade, 0);
		}
	}
}
