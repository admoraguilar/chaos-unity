using UnityEngine;

namespace ProjectCHAOS.Inputs
{
	public class TouchUIController : MonoBehaviour, IController
	{
		public Joystick joystick => _joystick;
		[SerializeField]
		private Joystick _joystick = null;

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
