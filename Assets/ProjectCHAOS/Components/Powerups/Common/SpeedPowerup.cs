using UnityEngine;

namespace ProjectCHAOS.Powerups
{
	[CreateAssetMenu(menuName = "ProjectCHAOS/Powerups/Speed")]
	public class SpeedPowerup : PowerupSpec
	{
		public float speedMultiplier = 1.5f;

		public override void Use()
		{
			Transform transform = references.Get<Transform>();
			Debug.Log($"using SpeedPowerup, {name} - {transform.name}, yay");
		}

		public override void Revoke()
		{
			
		}

		public override PowerupSpec Clone()
		{
			SpeedPowerup instance = CreateInstance<SpeedPowerup>();
			instance.speedMultiplier = speedMultiplier;
			return FinishClone(instance);
		}
	}
}
