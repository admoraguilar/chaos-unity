using System;
using UnityEngine;

namespace ProjectCHAOS.Gameplay.Behave
{
	[Serializable]
	public class CharacterMovement
	{
		public float moveSpeed = 15f;
		public float rotateSpeed = 500f;
		public float tackleSpeed = 24f;
		public float tackleLength = 4f;

		private Vector3 _currentMotion = Vector3.zero;
		private Vector3 _lastMotionWorldPosition = Vector3.zero;
		private Vector3 _currentRotation = Vector3.zero;
		private Vector3 _currentTackleDirection = Vector3.zero;
		private Vector3 _targetTackleWorldPosition = Vector3.zero;
		private bool _isDeployed = false;
		private bool _isTackling = false;

		private Transform _owner = null;

		public bool isDeployed => _isDeployed;

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

		public void Move(Vector3 motion)
		{
			_currentMotion = motion;
		}

		public void Deploy(bool active)
		{
			if(_isTackling) { return; }

			_isDeployed = active;
		}

		public void Tackle(Vector3 direction)
		{
			if(_isTackling) { return; }

			_currentTackleDirection = direction;
			_targetTackleWorldPosition = _lastMotionWorldPosition + (_currentTackleDirection * tackleLength);
			_isTackling = true;
		}

		public void FixedUpdate()
		{
			if(!_isTackling) {
				if(_currentMotion != Vector3.zero) {
					if(!_isDeployed) {
						owner.Translate(_currentMotion * moveSpeed * Time.deltaTime, Space.World);
					}

					Quaternion toRotation = Quaternion.LookRotation(_currentMotion, owner.up);
					owner.rotation = Quaternion.RotateTowards(owner.rotation, toRotation, rotateSpeed * Time.deltaTime);
				}

				_lastMotionWorldPosition = owner.position;
			} else {
				Vector3 tackleMotion = Vector3.Lerp(owner.position, _targetTackleWorldPosition, tackleSpeed * Time.deltaTime);
				owner.position = tackleMotion;

				if(Vector3.Distance(tackleMotion, _targetTackleWorldPosition) < .05f) {
					_isTackling = false;
				}
			}
		}
	}
}
