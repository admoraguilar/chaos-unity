using UnityEngine;

namespace ProjectCHAOS
{
    public class CharacterMechanic : MonoBehaviour
    {
		public float moveSpeed = 5f;
		public float rotateSpeed = 10f;

		private Vector3 _currentMotion = Vector3.zero;
		private Vector3 _currentRotation = Vector3.zero;

        private Transform _transform = null;

        public new Transform transform => this.GetCachedComponent(ref _transform);

		public void Move(Vector3 motion)
		{
			_currentMotion = motion;
		}

		private void FixedUpdate()
		{
			if(_currentMotion != Vector3.zero) { 
				transform.Translate(_currentMotion * moveSpeed * Time.deltaTime, Space.World);

				Quaternion toRotation = Quaternion.LookRotation(_currentMotion, transform.up);
				transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotateSpeed * Time.deltaTime);
			}
		}
	}
}
