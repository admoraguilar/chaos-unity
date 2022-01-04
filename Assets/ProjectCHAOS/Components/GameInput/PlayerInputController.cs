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

		private IMovementInputMap _movementInputMap = null;
		private ICombatInputMap _combatInputMap = null;

		public CharacterMechanic characterMechanic => this.GetCachedComponent(ref _characterMechanic);
		public MachineGun machineGun => this.GetCachedComponent(ref _machineGun);

		private void Awake()
		{
			_movementInputMap = MInput.GetPlayer(0).GetMap<IMovementInputMap>();
			_combatInputMap = MInput.GetPlayer(0).GetMap<ICombatInputMap>();
		}

		private void Update()
		{
			characterMechanic.Move(_movementInputMap.moveInputAxis);

			//if(_combatInputMap.isFiringDown) {
			//	machineGun.StartFiring();
			//}

			//if(_combatInputMap.isFiringUp) {
			//	machineGun.StopFiring();
			//}

			machineGun.StartFiring();

			if(_movementInputMap.didTap) {
				characterMechanic.Deploy(!characterMechanic.isDeployed);
			}

			if(_movementInputMap.didDoubleTap) {
				characterMechanic.Tackle(characterMechanic.transform.forward);
			}
		}
	}
}
