using UnityEngine;
using UnityEngine.UI;
using Lean.Touch;

namespace ProjectCHAOS.Inputs
{
	public class TouchUIController : MonoBehaviour, IController
	{
		[SerializeField]
		private Joystick _joystick = null;

		[SerializeField]
		private Image _swipeImage = null;

		[SerializeField]
		private LeanSwipeBase _swipe = null;

		[SerializeField]
		private Image _tapImage = null;
		
		[SerializeField]
		private LeanFingerTap _tap = null;

		public Joystick joystick => _joystick;
		public Image swipeImage => _swipeImage;
		public LeanSwipeBase swipeLean => _swipe;
		public Image tapImage => _tapImage;
		public LeanFingerTap tapLean => _tap;

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
