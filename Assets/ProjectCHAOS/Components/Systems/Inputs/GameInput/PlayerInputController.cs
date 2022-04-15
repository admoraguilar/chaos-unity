using UnityEngine;
using ProjectCHAOS.Gameplay.Weapons;
using ProjectCHAOS.Gameplay.Characters;

namespace ProjectCHAOS.Systems.Inputs.GameInputs
{
    public class PlayerInputController : MonoBehaviour
    {
		[Header("References")]
		[SerializeField]
		private CharacterMechanic _characterMechanic = null;
		
		[SerializeField]
		private WeaponHandler _weaponDriver = null;

		private IMovementInputMap _movementInputMap = null;
		private ICombatInputMap _combatInputMap = null;

		public CharacterMechanic characterMechanic => this.GetCachedComponent(ref _characterMechanic);
		public WeaponHandler weaponDriver => this.GetCachedComponent(ref _weaponDriver);

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

			weaponDriver.visualHolder.visual.StartFiring();

			if(_movementInputMap.didTap) {
				characterMechanic.Deploy(!characterMechanic.isDeployed);
			}

			if(_movementInputMap.didDoubleTap) {
				characterMechanic.Tackle(characterMechanic.transform.forward);
			}
		}
	}
}
