using System;
using UnityEngine;
using WaterToolkit;
using WaterToolkit.Spawners;

namespace ProjectCHAOS.Characters.AIs
{
	public class MobBehaviour : MonoBehaviour
	{
		public event Action OnFinish = delegate { }; 

		[SerializeField]
		private SerializableInterface<ISpawner> _spawner = null;

		public ISpawner spawner
		{
			get => _spawner.value;
			set => _spawner.value = value;
		}

		private Transform _transform = null;

		public new Transform transform => this.GetCachedComponent(ref _transform);

		private void OnSpawnerCycleFinish(int count)
		{
			if(count >= 1) { spawner.Stop(); }
		}

		private void OnSpawnerDespawn(GameObject instance)
		{
			if(spawner.cycleCount >= 1 && spawner.totalDespawnCount >= spawner.prefabList.Count) {
				OnFinish();
			}
		}

		public void Run()
		{
			spawner.OnCycleFinish += OnSpawnerCycleFinish;
			spawner.OnDespawn += OnSpawnerDespawn;
			spawner.Run();
		}

		public void Stop()
		{
			spawner.OnCycleFinish -= OnSpawnerCycleFinish;
			spawner.OnDespawn -= OnSpawnerDespawn;
			spawner.DespawnAll();
			spawner.Resetup(true);
		}
	}
}
