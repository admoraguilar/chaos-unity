using WaterToolkit;

namespace ProjectCHAOS.Powerups
{
	public abstract class PowerupSpec : FlyweightSpecObject<PowerupSpec>
	{
		public virtual void Use() { }

		public virtual void Revoke() { }
	}
}
