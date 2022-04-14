using System;
using System.Collections.Generic;
using UnityEngine;
using ProjectCHAOS.Systems;
using ProjectCHAOS.Gameplay.Behave;

namespace ProjectCHAOS.Gameplay.Spawners
{
    public class BasicSpawner : MonoBehaviour
    {
		public event Action<GameObject> OnSpawn = delegate { };

		[SerializeField]
        private List<GameObject> _toSpawnPrefabList = null;

		[SerializeField]
		private Timing _spawnRate = null;

		[SerializeField]
		private List<Transform> _spawnPointsList = new List<Transform>();

		[SerializeField]
		private bool _shouldSpawnOnStart = false;

		private List<GameObject> _spawnedList = new List<GameObject>();
		private bool _isSpawning = false;

		public List<GameObject> toSpawnPrefabList
		{
			get => _toSpawnPrefabList;
			set => _toSpawnPrefabList = value;
		}

		public Timing spawnRate
		{
			get => _spawnRate;
			private set => _spawnRate = value;
		}

		public List<Transform> spawnPoints
		{
			get => _spawnPointsList;
			private set => _spawnPointsList = value;
		}

		public bool shouldSpawnOnStart
		{
			get => _shouldSpawnOnStart;
			private set => _shouldSpawnOnStart = value;
		}

		public bool isSpawning
		{
			get => _isSpawning;
			private set => _isSpawning = value;
		}

		public void Run()
		{
			isSpawning = true;
			
		}

		public void Pause()
		{
			isSpawning = false;
		}

		public void Stop()
		{
			isSpawning = false;
			spawnRate.Reset();
		}

		public void DespawnAll()
		{
			_spawnedList.RemoveAll(go => go == null);
			foreach(GameObject go in _spawnedList) {
				Destroy(go);
			}
		}

		public void Spawn()
		{
			Transform spawnPoint = _spawnPointsList.Random();
			GameObject spawnedObject = Instantiate(_toSpawnPrefabList.Random(), spawnPoint, false);
			_spawnedList.Add(spawnedObject);

			OnSpawn(spawnedObject);
		}

		private void Start()
		{
			spawnRate.OnReachMax += Spawn;
		}

		private void OnDestroy()
		{
			spawnRate.OnReachMax -= Spawn;
		}

		private void FixedUpdate()
		{
			if(!isSpawning) { return; }
			spawnRate.Update();
		}
	}
}
