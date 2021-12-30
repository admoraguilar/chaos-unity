using UnityEngine;
using Lean.Touch;

namespace ProjectCHAOS.Inputs
{
	public class TouchUIController : MonoBehaviour, IController
	{
		public Joystick joystick => _joystick;
		[SerializeField]
		private Joystick _joystick = null;

		public LeanSwipeBase swipe => _swipe;
		[SerializeField]
		private LeanSwipeBase _swipe = null;

		public LeanFingerTap tap => _tap;
		[SerializeField]
		private LeanFingerTap _tap = null;

		private void OnEnable()
		{
			MInput.GetController<TouchUIController>(0).Initialize(this);
		}

		private void OnDisable()
		{
			MInput.GetController<TouchUIController>(0).Deinitialize();
		}
	}
}
