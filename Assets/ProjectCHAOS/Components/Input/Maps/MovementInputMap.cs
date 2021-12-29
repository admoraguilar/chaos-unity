using UnityEngine;

namespace ProjectCHAOS
{
	public interface IMovementInputMap
	{
		public Vector3 moveInputAxis { get; }
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
	}
}
