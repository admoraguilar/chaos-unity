using UnityEngine;
using WaterToolkit;
using WaterToolkit.Weapons;

namespace ProjectCHAOS.Characters.Players
{
	public class PlayerStatsLinker : MonoBehaviour
	{
		[SerializeField]
		private PlayerCharacter _playerCharacter = null;

		[SerializeField]
		private WeaponHandler _weaponHandler = null;

		public PlayerCharacter playerCharacter => this.GetCachedComponent(ref _playerCharacter);

		public WeaponHandler weaponHandler => this.GetCachedComponent(ref _weaponHandler);

		private void OnCharacterStatsSpeedChanged()
		{
			_playerCharacter.movement.speedMultiplier = _playerCharacter.characterStats.speed.current.modifiedValue;
		}

		private void OnPlayerStatsFireRateChanged()
		{
			_weaponHandler.visualHolder.fireRateMultiplier = _playerCharacter.characterStats.fireRate.current.modifiedValue;
		}

		private void OnEnable()
		{
			_playerCharacter.characterStats.speed.current.OnModifiersChanged += OnCharacterStatsSpeedChanged;
			_playerCharacter.characterStats.fireRate.current.OnModifiersChanged += OnPlayerStatsFireRateChanged;
		}

		private void OnDisable()
		{
			_playerCharacter.characterStats.speed.current.OnModifiersChanged -= OnCharacterStatsSpeedChanged;
			_playerCharacter.characterStats.fireRate.current.OnModifiersChanged -= OnPlayerStatsFireRateChanged;
		}

		private void Start()
		{
			OnCharacterStatsSpeedChanged();
			OnPlayerStatsFireRateChanged();
		}
	}
}
