using System;
using UnityEngine;
using WaterToolkit.Pickups;
using ProjectCHAOS.Upgrades;

namespace ProjectCHAOS.Pickups
{
	[CreateAssetMenu(menuName = "ProjectCHAOS/Pickups/Upgrade Cycler")]
	public class UpgradeCyclerPickup : PickupSpec
	{
		[NonSerialized]
		private Upgrader _upgrader = null;

		protected override void OnInitialize()
		{
			_upgrader = references.Get<Upgrader>();
		}

		public override void Pickup() 
		{
			_upgrader.objectIndex++;
		}

		public override PickupSpec Clone()
		{
			UpgradeCyclerPickup upgradeCyclerPickupScec = new UpgradeCyclerPickup();
			return FinishClone(upgradeCyclerPickupScec);
		}
	}
}
