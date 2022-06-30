using System.Collections.Generic;
using UnityEngine;

namespace WaterToolkit.Pickups
{
	public abstract class PickupSpec : FlyweightSpecObject<PickupSpec>
	{
		public abstract void Pickup();
	}
}
