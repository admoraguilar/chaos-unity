using System;
using UnityEngine;

namespace ProjectCHAOS.Gameplay.Behave
{
	[Serializable]
	public class SimpleMovement
	{
		private Vector3 _direction = Vector3.zero;
		private float _speed = 0f;
		private Space _space = Space.Self;
		private Transform _owner = null;

		public Vector3 direction
		{
			get => _direction;
			set => _direction = value;
		}

		public float speed
		{
			get => _speed;
			set => _speed = value;
		}

		public Space space
		{
			get => _space;
			set => _space = value;
		}

		public Transform owner
		{
			get => _owner;
			set => _owner = value;
		}

		public bool isFunctional
		{
			get => owner != null;
		}

		public void Initialize(Transform owner)
		{
			this.owner = owner;
		}

		public void Update()
		{
			if(!isFunctional) { return; }

			owner.Translate(direction * speed * Time.deltaTime, space);
		}
	}
}
