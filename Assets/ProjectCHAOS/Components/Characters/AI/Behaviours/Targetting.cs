using System;
using UnityEngine;

namespace ProjectCHAOS.Characters.AIs
{
	[Serializable]
	public class Targetting
	{
		public Transform target;
		public Vector3 targetPoint;

		private Transform _owner = null;

		public void Initialize(Transform owner)
		{
			_owner = owner;
		}

		public Vector3 GetDirectionToTarget()
		{
			//if(target == null) {
			//	throw new NullReferenceException("Targetting has no target, please set one.");
			//}

			Vector3 point = target == null ? targetPoint : target.position;
			Vector3 dir = point - _owner.position;
			return dir.normalized;
		}

		public float GetDistanceToTaget()
		{
			Vector3 point = target == null ? targetPoint : target.position;
			return Vector3.Distance(point, _owner.position);
		}
	}
}
