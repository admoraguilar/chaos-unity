using UnityEngine;

namespace ProjectCHAOS.Powerups
{
	[CreateAssetMenu(menuName = "ProjectCHAOS/Powerups/Speed")]
	public class SpeedPowerup : PowerupSpec
	{
		public float speedMultiplier = 1.5f;

		public override void Use()
		{
			Debug.Log($"using SpeedPowerup, {name}, yay");
		}

		public override PowerupSpec Clone()
		{
			SpeedPowerup instance = CreateInstance<SpeedPowerup>();
			instance.speedMultiplier = speedMultiplier;
			return FinishClone(instance);
		}
	}
}
