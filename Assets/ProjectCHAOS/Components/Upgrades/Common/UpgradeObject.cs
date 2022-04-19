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

		protected override int maxIndex => behaviours.Count;

		protected override void OnUpgrade(Transform toUpgrade, int forceIndex = -2)
		{
			behaviours[forceIndex].Upgrade(toUpgrade);
		}
	}

	public abstract class UpgradeObject : ScriptableObject
	{
		public new string name = string.Empty;

		[NonSerialized]
		private int _index = -1;

		protected abstract int maxIndex { get; }

		public int index
		{
			get => _index;
			set {
				index = Mathf.Clamp(value, -1, maxIndex);
			}
		}

		public void Upgrade(Transform toUpgrade, int forceIndex = -2)
		{
			if(forceIndex < -1) { index++; } else { index = forceIndex; }
			OnUpgrade(toUpgrade, index);
		}

		protected abstract void OnUpgrade(Transform toUpgrade, int forceIndex = -2);

		public void Reset(Transform toUpgrade)
		{
			Upgrade(toUpgrade, -1);
		}
	}
}
