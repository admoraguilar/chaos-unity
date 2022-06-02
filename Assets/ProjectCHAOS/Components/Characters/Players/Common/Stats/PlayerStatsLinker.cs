using UnityEngine;
using WaterToolkit;
using WaterToolkit.Weapons;

namespace WaterToolkit.Characters.Players
{
	public class PlayerStatsLinker : MonoBehaviour
	{
		[SerializeField]
		private PlayerCharacter _playerCharacter = null;

		[SerializeField]
		private WeaponHandler _weaponHandler = null;

		public PlayerCharacter playerCharacter => this.GetCachedComponent(ref _playerCharacter);

		public WeaponHandler weaponHandler => this.GetCachedComponent(ref _weaponHandler);

		private void OnCharacterStatsSpeedChanged(float value)
		{
			_playerCharacter.movement.speedMultiplier = value;
		}

		private void OnPlayerStatsFireRateChanged(float value)
		{
			_weaponHandler.visualHolder.fireRateMultiplier = value;
		}

		private void OnEnable()
		{
			_playerCharacter.characterStats.speed.OnValueChange += OnCharacterStatsSpeedChanged;
			_playerCharacter.characterStats.fireRate.OnValueChange += OnPlayerStatsFireRateChanged;
		}

		private void OnDisable()
		{
			_playerCharacter.characterStats.speed.OnValueChange -= OnCharacterStatsSpeedChanged;
			_playerCharacter.characterStats.fireRate.OnValueChange -= OnPlayerStatsFireRateChanged;
		}

		private void Start()
		{
			OnCharacterStatsSpeedChanged(_playerCharacter.characterStats.speed.value);
			OnPlayerStatsFireRateChanged(_playerCharacter.characterStats.fireRate.value);
		}
	}
}
