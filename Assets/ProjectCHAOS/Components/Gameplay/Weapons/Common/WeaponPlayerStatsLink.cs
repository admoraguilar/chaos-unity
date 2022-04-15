using UnityEngine;
using ProjectCHAOS.Gameplay.Characters.Players;

namespace ProjectCHAOS.Gameplay.Weapons
{
	public class WeaponPlayerStatsLink : MonoBehaviour
	{
		[SerializeField]
		private WeaponHandler _handler = null;

		[SerializeField]
		private PlayerCharacter _playerCharacter = null;

		private void OnPlayerStatsFireRateChanged(float value)
		{
			_handler.visualHolder.fireRateMultiplier = value;
		}

		private void OnEnable()
		{
			_playerCharacter.characterStats.fireRate.OnValueChange += OnPlayerStatsFireRateChanged;
		}

		private void OnDisable()
		{
			_playerCharacter.characterStats.fireRate.OnValueChange -= OnPlayerStatsFireRateChanged;
		}

		private void Start()
		{
			OnPlayerStatsFireRateChanged(_playerCharacter.characterStats.fireRate.value);
		}
	}
}
