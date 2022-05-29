using System;
using UnityEngine;
using ProjectCHAOS.Worlds;

namespace ProjectCHAOS.Behave
{
	[Serializable]
	public class Positioning
	{
		private Collider _ownerCollider = null;
		private LevelArea _levelArea = null;

		public Collider ownerCollider
		{
			get => _ownerCollider;
			set => _ownerCollider = value;
		}

		public LevelArea levelArea
		{
			get => _levelArea;
			set => _levelArea = value;
		}

		public bool isFunctional
		{
			get => levelArea != null;
		}

		public void Initialize(LevelArea levelArea)
		{
			this.levelArea = levelArea;
		}

		public Vector3 GetPosition()
		{
			if(!isFunctional) { return Vector3.zero; }

			Vector3 position = _levelArea.GetRandomPointXZ();
			float yOffset = 0f;

			if(_ownerCollider != null) {
				Vector3 ownerCenter = _ownerCollider.bounds.center;
				Vector3 ownerMin = _ownerCollider.bounds.min;
				yOffset = Mathf.Abs(ownerCenter.y - ownerMin.y);
			}

			return new Vector3(position.x, position.y + yOffset, position.z);
		}
	}
}
