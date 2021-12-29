using UnityEngine;

namespace ProjectCHAOS
{
    public class CharacterMechanic : MonoBehaviour
    {
		public float moveSpeed = 5f;
		public float rotateSpeed = 10f;

		private bool _isDeployed = false;
		private Vector3 _currentMotion = Vector3.zero;
		private Vector3 _currentRotation = Vector3.zero;

        private Transform _transform = null;

		public bool isDeployed => _isDeployed;

        public new Transform transform => this.GetCachedComponent(ref _transform);

		public void Move(Vector3 motion)
		{
			_currentMotion = motion;
		}

		public void Deploy(bool active)
		{
			_isDeployed = active;
		}

		private void FixedUpdate()
		{
			if(_currentMotion != Vector3.zero) {
				if(!_isDeployed) {
					transform.Translate(_currentMotion * moveSpeed * Time.deltaTime, Space.World);
				}
				
				Quaternion toRotation = Quaternion.LookRotation(_currentMotion, transform.up);
				transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotateSpeed * Time.deltaTime);
			}
		}
	}
}
