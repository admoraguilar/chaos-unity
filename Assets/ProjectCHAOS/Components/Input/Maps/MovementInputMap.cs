using UnityEngine;

namespace ProjectCHAOS
{
	public interface IMovementInputMap
	{
		public Vector3 moveInputAxis { get; }
		public bool didTap { get; }
	}

	public class PCMovementInputMap : IMovementInputMap
	{
		private Vector3 _moveInputAxis = Vector3.zero;

		public Vector3 moveInputAxis
		{
			get {
				_moveInputAxis.x = Input.GetAxisRaw("Horizontal");
				_moveInputAxis.z = Input.GetAxisRaw("Vertical");
				return _moveInputAxis;
			}
		}

		public bool didTap => Input.GetMouseButtonUp(0);
	}

	public class MobileMovementInputMap : IMovementInputMap
	{
		private Joystick _joystick = null;
		private Vector3 _moveInputAxis = Vector3.zero;

		public Vector3 moveInputAxis
		{
			get {
				if(_joystick == null) {
					_joystick = Object.FindObjectOfType<Joystick>();
				}

				_moveInputAxis.x = _joystick.Horizontal;
				_moveInputAxis.z = _joystick.Vertical;
				return _moveInputAxis;
			}
		}

		public bool didTap => Input.GetMouseButtonUp(0);
	}
}
