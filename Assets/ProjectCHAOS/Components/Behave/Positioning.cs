using System;
using ProjectCHAOS.Levels;
using UnityEngine;

namespace ProjectCHAOS.Behave
{
	[Serializable]
	public class Positioning
	{
		private Collider _ownerCollider = null;
		private LevelArea _levelArea = null;

		public void Initialize(Collider ownerCollider, LevelArea levelArea)
		{
			_ownerCollider = ownerCollider;
			_levelArea = levelArea;
		}

		public Vector3 GetPosition()
		{
			Vector3 position = _levelArea.GetRandomPointXZ();
			Vector3 ownerCenter = _ownerCollider.bounds.center;
			Vector3 ownerMin = _ownerCollider.bounds.min;

			return new Vector3(
				position.x,
				position.y + Mathf.Abs(ownerCenter.y - ownerMin.y),
				position.z);
		}
	}
}
