using UnityEngine;
using ProjectCHAOS.Gameplay.Weapons;
using ProjectCHAOS.Gameplay.Characters.Players;

namespace ProjectCHAOS.Systems.Inputs.GameInputs
{
    public class PlayerInputController : MonoBehaviour
    {
		[Header("References")]
		[SerializeField]
		private PlayerCharacter _playerCharacter = null;
		
		[SerializeField]
		private WeaponHandler _weaponHandler = null;

		private IMovementInputMap _movementInputMap = null;
		private ICombatInputMap _combatInputMap = null;

		public PlayerCharacter playerCharacter => this.GetCachedComponent(ref _playerCharacter);
		public WeaponHandler weaponHandler => this.GetCachedComponent(ref _weaponHandler);

		private void Awake()
		{
			_movementInputMap = MInput.GetPlayer(0).GetMap<IMovementInputMap>();
			_combatInputMap = MInput.GetPlayer(0).GetMap<ICombatInputMap>();
		}

		private void Update()
		{
			playerCharacter.movement.Move(_movementInputMap.moveInputAxis);

			weaponHandler.visualHolder.StartFiring();

			if(_movementInputMap.didTap) {
				playerCharacter.movement.Deploy(!playerCharacter.movement.isDeployed);
			}

			if(_movementInputMap.didDoubleTap) {
				playerCharacter.movement.Tackle(playerCharacter.transform.forward);
			}
		}
	}
}
