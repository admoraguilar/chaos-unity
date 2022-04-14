using System;
using UnityEngine;

namespace ProjectCHAOS.Gameplay.Behave
{
	[Serializable]
	public class Targetting
	{
		private Transform _target = null;
		private Vector3 _targetPoint = Vector3.zero;
		private Transform _owner = null;

		public Transform target
		{
			get => _target;
			set => _target = value;
		}

		public Vector3 targetPoint
		{
			get {
				_targetPoint = target == null ? _targetPoint : target.position;
				return _targetPoint;
			}
			set {
				_target = null;
				_targetPoint = value;
			}
		}

		public Transform owner
		{
			get => _owner;
			private set => _owner = value;
		}

		public bool isFunctional
		{
			get => owner != null;
		}

		public void Initialize(Transform owner)
		{
			this.owner = owner;
		}

		public Vector3 GetDirectionToTarget()
		{
			if(!isFunctional) { return Vector3.zero; }

			Vector3 dir = targetPoint - owner.position;
			dir.y = 0f;
			return dir.normalized;
		}

		public float GetDistanceToTarget()
		{
			if(!isFunctional) { return 0f; }

			return Vector3.Distance(targetPoint, owner.position);
		}
	}
}
