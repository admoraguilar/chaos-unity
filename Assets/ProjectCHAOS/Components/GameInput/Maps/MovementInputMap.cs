using UnityEngine;

namespace ProjectCHAOS.Inputs
{
	public interface IMovementInputMap : IMap
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

		public void Initialize() { }
		public void Deinitialize() { }
		public void Update() { }
		public void FixedUpdate() { }
		public void LateUpdate() { }
	}

	public class MobileMovementInputMap : IMovementInputMap
	{
		private Controller<TouchUIController> _touchUI = null;

		public Vector3 moveInputAxis
		{
			get {
				if(!_touchUI.isReady) {
					return Vector3.zero;
				}

				_moveInputAxis.x = _touchUI.controller.joystick.Horizontal;
				_moveInputAxis.z = _touchUI.controller.joystick.Vertical;
				return _moveInputAxis;
			}
		}

		private Vector3 _moveInputAxis = Vector3.zero;

		public bool didTap => _didTap;
		private bool _didTap = false;

		public void Initialize() 
		{
			_touchUI = MInput.GetController<TouchUIController>(0);
			_touchUI.controller.swipe.OnDelta.AddListener(OnSwipeDelta);
			_touchUI.controller.tap.OnCount.AddListener(OnTapCount);
		}

		public void Deinitialize() 
		{
			_touchUI.controller.swipe.OnDelta.RemoveListener(OnSwipeDelta);
			_touchUI.controller.tap.OnCount.RemoveListener(OnTapCount);
		}
		
		private void OnSwipeDelta(Vector2 delta)
		{
			Debug.Log($"Swipe: {delta}");
		}
		
		private void OnTapCount(int count)
		{
			Debug.Log($"Tap: {count}");
			_didTap = true;
		}

		public void Update() { }
		
		public void FixedUpdate() { }
		
		public void LateUpdate() 
		{
			_didTap = false;
		}
	}
}
