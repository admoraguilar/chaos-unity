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
		private Image _tapImage = null;

		[SerializeField]
		private LeanSwipeBase _swipe = null;

		[SerializeField]
		private LeanFingerTap _tap = null;

		public Joystick joystick
		{
			get => _joystick;
			private set => joystick = value;
		}

		public Image swipeImage
		{
			get => _swipeImage;
			private set => _swipeImage = value;
		}

		public LeanSwipeBase swipe
		{
			get => _swipe;
			private set => _swipe = value;
		}

		public Image tapImage
		{
			get => _tapImage;
			private set => _tapImage = value;
		}

		public LeanFingerTap tap
		{
			get => _tap;
			private set => _tap = value;
		}

		public void Initialize(LeanSwipeBase swipe, LeanFingerTap tap)
		{
			this.swipe = swipe;
			this.tap = tap;
		}

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
