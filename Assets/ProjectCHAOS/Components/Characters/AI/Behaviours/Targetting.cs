using System;
using UnityEngine;

namespace ProjectCHAOS.Characters.AIs
{
	[Serializable]
	public class Targetting
	{
		public Transform target;

		private Transform _owner = null;

		public void Initialize(Transform owner)
		{
			_owner = owner;
		}

		public Vector3 GetDirectionToTarget()
		{
			if(target == null) {
				throw new NullReferenceException("Targetting has no target, please set one.");
			}

			Vector3 dir = target.position - _owner.position;
			return dir.normalized;
		}
	}
}
