using UnityEngine;
using WaterToolkit.Behave;
using ProjectCHAOS.Characters.Players;

namespace ProjectCHAOS.Powerups
{
	[CreateAssetMenu(menuName = "ProjectCHAOS/Powerups/Fire Rate")]
	public class FireRatePowerup : PowerupSpec
	{
		public float multiplier = 1.5f;

		public override void Use()
		{
			PlayerCharacter character = references.Get<PlayerCharacter>();
			character.characterStats.fireRate.current.ReplaceModifier(
				new FloatMultiplierModifier {
					id = nameof(FireRatePowerup),
					multiplier = multiplier
				});
		}

		public override void Revoke()
		{
			PlayerCharacter character = references.Get<PlayerCharacter>();
			character.characterStats.fireRate.current.RemoveModifier(
				new FloatMultiplierModifier {
					id = nameof(FireRatePowerup),
					multiplier = multiplier
				});
		}

		public override PowerupSpec Clone()
		{
			FireRatePowerup fireRatePowerup = CreateInstance<FireRatePowerup>();
			fireRatePowerup.multiplier = multiplier;
			return FinishClone(fireRatePowerup);
		}
	}
}
