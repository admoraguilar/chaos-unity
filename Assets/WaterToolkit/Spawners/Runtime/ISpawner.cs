using System;
using System.Collections.Generic;
using UnityEngine;

namespace WaterToolkit.Spawners
{
	public interface ISpawner
	{
		public event Action<GameObject> OnSpawn;
		public event Action<GameObject> OnDespawn;
		public event Action<int> OnCycleFinish;

		public List<GameObject> prefabList { get; }

		public List<GameObject> instanceList { get; }

		public int cycleCount { get; }
		
		public int totalSpawnCount { get; }

		public int totalDespawnCount { get; }

		public void Run();

		public void Pause();

		public void Stop();

		public void Resetup(bool shouldStop = false);

		GameObject Spawn();

		IReadOnlyCollection<GameObject> DespawnAll();
	}
}
