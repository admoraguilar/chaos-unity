using UnityEngine;
using WaterToolkit.Procedural;

namespace WaterToolkit.Spawners
{
	public class PointSpawner : SimpleSpawner
	{
		[SerializeField]
		private SerializableInterface<IPointProvider> _pointProvider = null;

		public IPointProvider pointProvider
		{
			get => _pointProvider.value;
			set => _pointProvider.value = value;
		}

		public override GameObject Spawn()
		{
			GameObject instance = base.Spawn();
			instance.transform.position = pointProvider.GetRandomPoint();
			return instance;
		}
	}
}
