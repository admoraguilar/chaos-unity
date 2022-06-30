using UnityEngine;

namespace WaterToolkit.Weapons
{
	public struct BulletLaunchInfo
	{
		public Transform owner;
		public Transform targetTransform;
		public Vector3 targetPoint;
		public Vector3 direction;
	}
}