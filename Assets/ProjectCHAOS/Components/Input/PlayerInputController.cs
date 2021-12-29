using UnityEngine;

namespace ProjectCHAOS.Inputs
{
    public class PlayerInputController : MonoBehaviour
    {
		[Header("References")]
		[SerializeField]
		private CharacterMechanic _characterMechanic = null;
		
		[SerializeField]
		private MachineGun _machineGun = null;

		public CharacterMechanic characterMechanic => this.GetCachedComponent(ref _characterMechanic);
		public MachineGun machineGun => this.GetCachedComponent(ref _machineGun);

		private void Update()
		{
			characterMechanic.Move(GameInput.movementInputMap.moveInputAxis);

			//if(GameInput.combatInputMap.isFiringDown) {
			//	machineGun.StartFiring();
			//}

			//if(GameInput.combatInputMap.isFiringUp) {
			//	machineGun.StopFiring();
			//}

			machineGun.StartFiring();
		}
	}
}
