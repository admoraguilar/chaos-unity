using UnityEngine;
using ProjectCHAOS.Characters.Players;
using WaterToolkit.Behave;

namespace ProjectCHAOS.Powerups
{
	[CreateAssetMenu(menuName = "ProjectCHAOS/Powerups/Speed")]
	public class SpeedPowerup : PowerupSpec
	{
		public float multiplier = 1.5f;

		public override void Use()
		{
			PlayerCharacter character = references.Get<PlayerCharacter>();
			character.characterStats.speed.current.AddModifier(
				new FloatMultiplierModifier {
					id = nameof(FireRatePowerup),
					multiplier = multiplier
				});
		}

		public override void Revoke()
		{
			PlayerCharacter character = references.Get<PlayerCharacter>();
			character.characterStats.speed.current.RemoveModifier(
				new FloatMultiplierModifier {
					id = nameof(FireRatePowerup),
					multiplier = multiplier
				});
		}

		public override PowerupSpec Clone()
		{
			SpeedPowerup instance = CreateInstance<SpeedPowerup>();
			instance.multiplier = multiplier;
			return FinishClone(instance);
		}
	}
}
