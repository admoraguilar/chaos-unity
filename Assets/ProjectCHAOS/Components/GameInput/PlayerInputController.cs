using UnityEngine;
using WaterToolkit.GameInputs;
using WaterToolkit.Weapons;
using WaterToolkit.Characters.Players;

namespace WaterToolkit.GameInputs
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
			_movementInputMap = GameInput.GetPlayer(0).GetMap<IMovementInputMap>();
			_combatInputMap = GameInput.GetPlayer(0).GetMap<ICombatInputMap>();
		}

		private void Update()
		{
			playerCharacter.movement.Move(_movementInputMap.moveInputAxis);

			weaponHandler.visualHolder.StartFiring();

			//if(_movementInputMap.didTap) {
			//	playerCharacter.movement.Deploy(!playerCharacter.movement.isDeployed);
			//}

			//if(_movementInputMap.didDoubleTap) {
			//	playerCharacter.movement.Tackle(playerCharacter.transform.forward);
			//}
		}
	}
}
