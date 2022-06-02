using System;
using UnityEngine;
using WaterToolkit.Upgrades;

namespace WaterToolkit.Pickups
{
	[CreateAssetMenu(menuName = "ProjectCHAOS/Pickups/Upgrade Cycler")]
	public class UpgradeCyclerPickupObject : PickupObject
	{
		[NonSerialized]
		private Upgrader _upgrader = null;

		public override void Initialize(Transform reference)
		{
			Upgrader upgrader = reference.GetComponentInChildren<Upgrader>();
			if(upgrader != null) { _upgrader = upgrader; }
		}

		public override void Pickup(Transform owner) 
		{
			_upgrader.objectIndex++;
		}
	}
}
