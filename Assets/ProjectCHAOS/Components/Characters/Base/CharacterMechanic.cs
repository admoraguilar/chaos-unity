using UnityEngine;

namespace ProjectCHAOS
{
    public class CharacterMechanic : MonoBehaviour
    {
		public float moveSpeed = 5f;
		public float rotateSpeed = 10f;
		public float tackleSpeed = 15f;
		public float tackleLength = 8f;

		private Vector3 _currentMotion = Vector3.zero;
		private Vector3 _lastMotionWorldPosition = Vector3.zero;
		private Vector3 _currentRotation = Vector3.zero;
		private Vector3 _currentTackleDirection = Vector3.zero;
		private Vector3 _targetTackleWorldPosition = Vector3.zero;
		private bool _isDeployed = false;
		private bool _isTackling = false;

		private Transform _transform = null;

		public bool isDeployed => _isDeployed;

        public new Transform transform => this.GetCachedComponent(ref _transform);

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

		private void FixedUpdate()
		{
			if(!_isTackling) {
				if(_currentMotion != Vector3.zero) {
					if(!_isDeployed) {
						transform.Translate(_currentMotion * moveSpeed * Time.deltaTime, Space.World);
					}

					Quaternion toRotation = Quaternion.LookRotation(_currentMotion, transform.up);
					transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotateSpeed * Time.deltaTime);
				}

				_lastMotionWorldPosition = transform.position;
			} else {
				Vector3 tackleMotion = Vector3.Lerp(transform.position, _targetTackleWorldPosition, tackleSpeed * Time.deltaTime);
				transform.position = tackleMotion;

				if(Vector3.Distance(tackleMotion, _targetTackleWorldPosition) < .05f) {
					_isTackling = false;
				}
			}
		}
	}
}
