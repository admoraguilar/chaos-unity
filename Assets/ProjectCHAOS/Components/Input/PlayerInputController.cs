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

		private IMovementInputMap _movementInputMap = null;
		private ICombatInputMap _combatInputMap = null;

		private void Awake()
		{
			_movementInputMap = GameInput.GetPlayer(0).GetMap<IMovementInputMap>();
			_combatInputMap = GameInput.GetPlayer(0).GetMap<ICombatInputMap>();
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
				Debug.Log("Did tap");
				characterMechanic.Deploy(!characterMechanic.isDeployed);
			}
		}
	}
}
