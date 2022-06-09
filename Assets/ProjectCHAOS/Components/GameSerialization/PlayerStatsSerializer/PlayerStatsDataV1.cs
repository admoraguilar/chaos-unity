using System;
using WaterToolkit.DataSerialization;

namespace ProjectCHAOS.GameSerialization
{
	[Serializable]
	public class PlayerStatsDataV1 : IObjectVersion
	{
		public float speed = 1f;
		public float fireRate = 1f;

		public int objectVersion => 0;

		public IObjectVersion ToPrev() => null;

		public IObjectVersion ToNext() => null;
	}
}
