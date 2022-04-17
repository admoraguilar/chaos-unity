using System;
using UnityEngine;

namespace ProjectCHAOS.Gameplay.Behave
{
	[Serializable]
	public class Targetting
	{
		public static Vector3 CalculateDirectionToTarget(Transform from, Transform to) =>
			CalculateDirectionToTarget(from.position, to.position);

		public static Vector3 CalculateDirectionToTarget(Vector3 from, Vector3 to)
		{
			Vector3 result = from - to;
			return result.normalized;
		}

		public static Transform GetNearestTransform(
			Vector3 origin, float radius, 
			int layerMask, Transform owner = null)
		{
			Collider[] results = Physics.OverlapSphere(origin, radius, layerMask);
			Transform target = null;

			if(results.Length > 0) {
				target = results[0].transform;
			}

			foreach(Collider result in results) {
				if(result.transform == owner) {
					continue;
				}

				Transform resultTransform = result.transform;
				float targetDistance = Vector3.Distance(origin, target.position);
				float resultDistance = Vector3.Distance(origin, resultTransform.position);
				if(resultDistance < targetDistance) {
					target = resultTransform;
				}
			}

			return target;
		}


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

			Vector3 dir = CalculateDirectionToTarget(targetPoint, owner.position);
			dir.y = 0f;
			return dir;
		}

		public float GetDistanceToTarget()
		{
			if(!isFunctional) { return 0f; }

			return Vector3.Distance(targetPoint, owner.position);
		}
	}
}
