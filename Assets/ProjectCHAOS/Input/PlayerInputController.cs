using UnityEngine;
using ProjectCHAOS.Weapons;
using ProjectCHAOS.Utilities;
using ProjectCHAOS.Characters;

namespace ProjectCHAOS.Inputs
{
    public class PlayerInputController : MonoBehaviour
    {
        private Vector3 _moveInputAxis = Vector3.zero;

		private CharacterMechanic _characterMechanic = null;
		private MachineGun _machineGun = null;

		public CharacterMechanic characterMechanic => this.GetCachedComponent(ref _characterMechanic);
		public MachineGun machineGun => this.GetCachedComponent(ref _machineGun);

		private void Update()
		{
			_moveInputAxis.x = Input.GetAxisRaw("Horizontal");
			_moveInputAxis.z = Input.GetAxisRaw("Vertical");

			characterMechanic.Move(_moveInputAxis);

			if(Input.GetKeyDown(KeyCode.Mouse0)) {
				machineGun.StartFiring();
			}

			if(Input.GetKeyUp(KeyCode.Mouse0)) {
				machineGun.StopFiring();
			}
		}
	}
}
