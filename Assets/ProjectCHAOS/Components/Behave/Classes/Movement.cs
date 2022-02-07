using System;
using UnityEngine;

namespace ProjectCHAOS.Behave
{
	[Serializable]
	public class Movement
	{
		[SerializeField]
		private Vector3 _direction = Vector3.zero;

		[SerializeField]
		private float _speed = 10f;

		[SerializeField]
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

		public void Initialize(Transform owner)
		{
			_owner = owner;
		}

		public void Update()
		{
			_owner.Translate(direction * speed * Time.deltaTime, _space);
		}
	}
}
