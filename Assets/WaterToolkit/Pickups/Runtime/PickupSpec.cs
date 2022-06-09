using System.Collections.Generic;
using UnityEngine;

namespace WaterToolkit.Pickups
{
	public abstract class PickupSpec : FlyweightSpec<PickupSpec>
	{
		public abstract void Pickup();
	}
}
