using WaterToolkit;

namespace ProjectCHAOS.Powerups
{
	public abstract class PowerupSpec : FlyweightSpec<PowerupSpec>
	{
		public virtual void Use() { }

		public virtual void Revoke() { }
	}
}
