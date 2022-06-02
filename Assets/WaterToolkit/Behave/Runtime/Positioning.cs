using System;
using UnityEngine;

namespace WaterToolkit.Behave
{
	public interface IPositionProvider
	{
		Vector3 GetRandomPointXZ();
	}

	[Serializable]
	public class Positioning
	{
		private Collider _ownerCollider = null;
		private IPositionProvider _positionProvider = null;

		public Collider ownerCollider
		{
			get => _ownerCollider;
			set => _ownerCollider = value;
		}

		public IPositionProvider positionProvider
		{
			get => _positionProvider;
			set => _positionProvider = value;
		}

		public bool isFunctional
		{
			get => positionProvider != null;
		}

		public void Initialize(IPositionProvider positionProvider)
		{
			this.positionProvider = positionProvider;
		}

		public Vector3 GetPosition()
		{
			if(!isFunctional) { return Vector3.zero; }

			Vector3 position = _positionProvider.GetRandomPointXZ();
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
