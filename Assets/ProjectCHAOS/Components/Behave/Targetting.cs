using System;
using UnityEngine;

namespace ProjectCHAOS.Behave
{
	[Serializable]
	public class Targetting
	{
		private Transform _target = null;
		private Vector3 _targetPoint = Vector3.zero;

		private Transform _owner = null;

		public Transform target => _target;
		public Vector3 targetPoint => _targetPoint;

		public void Initialize(Transform owner)
		{
			_owner = owner;
		}

		public void SetTarget(Transform target)
		{
			_target = target;
		}

		public void SetTarget(Vector3 target)
		{
			_target = null;
			_targetPoint = target;
		}

		public Vector3 GetDirectionToTarget()
		{
			Vector3 point = target == null ? targetPoint : target.position;
			Vector3 dir = point - _owner.position;
			return dir.normalized;
		}

		public float GetDistanceToTarget()
		{
			Vector3 point = target == null ? targetPoint : target.position;
			return Vector3.Distance(point, _owner.position);
		}
	}
}
