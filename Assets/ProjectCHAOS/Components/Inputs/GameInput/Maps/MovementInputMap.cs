using UnityEngine;
using Lean.Touch;

namespace ProjectCHAOS.Inputs.GameInputs
{
	public interface IMovementInputMap : IMap
	{
		public Vector3 moveInputAxis { get; }
		public bool didTap { get; }
		public bool didDoubleTap { get; }
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
		public bool didDoubleTap => false;

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
		private bool _didTap = false;
		private bool _didDoubleTap = false;

		public bool didTap => _didTap;
		public bool didDoubleTap => _didDoubleTap;

		public void Initialize() 
		{
			_touchUI = MInput.GetController<TouchUIController>(0);
			_touchUI.OnInitialize += OnTouchUIInitialize;
			_touchUI.OnDeinitialize += OnTouchUIDeinitialize;
		}

		public void Deinitialize() 
		{
			_touchUI = MInput.GetController<TouchUIController>(0);
			_touchUI.OnInitialize -= OnTouchUIInitialize;
			_touchUI.OnDeinitialize -= OnTouchUIDeinitialize;
		}
		
		private void OnSwipeFinger(LeanFinger finger)
		{
			bool isInImage = RectTransformUtility.RectangleContainsScreenPoint(
				_touchUI.controller.swipeImage.rectTransform, 
				finger.StartScreenPosition);
			if(!isInImage) { return; }

			if(finger.Swipe) { _didDoubleTap = true; }
		}

		private void OnTapFinger(LeanFinger finger)
		{
			bool isInImage = RectTransformUtility.RectangleContainsScreenPoint(
				_touchUI.controller.tapImage.rectTransform, 
				finger.StartScreenPosition);
			if(!isInImage) { return; }

			if(finger.TapCount == 1) { _didTap = true; }
		}

		private void OnTouchUIInitialize()
		{
			_touchUI.controller.swipe.OnFinger.AddListener(OnSwipeFinger);
			_touchUI.controller.tap.OnFinger.AddListener(OnTapFinger);
		}

		private void OnTouchUIDeinitialize()
		{
			_touchUI.controller.swipe.OnFinger.RemoveListener(OnSwipeFinger);
			_touchUI.controller.tap.OnFinger.RemoveListener(OnTapFinger);
		}

		public void Update() { }
		
		public void FixedUpdate() { }
		
		public void LateUpdate() 
		{
			_didTap = false;
			_didDoubleTap = false;
		}
	}
}
