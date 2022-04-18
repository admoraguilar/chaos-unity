using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace ProjectCHAOS.Systems.Inputs
{
	public class TouchUIController : MonoBehaviour, IController
	{
		[SerializeField]
		private Joystick _joystick = null;

		[SerializeField]
		private Image _swipeImage = null;

		[SerializeField]
		private Image _tapImage = null;

		[Header("External References")]
		[SerializeField]
		private LeanTouchInput _leanTouchInput = null;

		public Joystick joystick => _joystick;

		public Image swipeImage => _swipeImage;
		
		public Image tapImage => _tapImage;
		
		public LeanTouchInput leanTouchInput
		{
			get => _leanTouchInput;
			private set => _leanTouchInput = value;
		}

		public void Initialize(LeanTouchInput leanTouchInput)
		{
			this.leanTouchInput = leanTouchInput;
		}

		public void SimulateOnPointerUp()
		{
			joystick.OnPointerUp(new PointerEventData(EventSystem.current));
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
