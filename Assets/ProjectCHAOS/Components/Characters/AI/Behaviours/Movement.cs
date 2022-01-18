using System;
using UnityEngine;

namespace ProjectCHAOS.Characters.AIs
{
	[Serializable]
	public class Movement
	{
		public Vector3 direction = Vector3.zero;
		public float speed = 10f;

		private Transform _owner = null;

		public void Initialize(Transform owner)
		{
			_owner = owner;
		}

		public void FixedUpdate()
		{
			_owner.Translate(direction * speed * Time.deltaTime, Space.Self);
		}
	}
}
