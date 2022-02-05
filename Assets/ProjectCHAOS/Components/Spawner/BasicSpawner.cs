using System.Collections.Generic;
using UnityEngine;
using ProjectCHAOS.Behave;
using ProjectCHAOS.Common;

namespace ProjectCHAOS.Spawners
{
    public class BasicSpawner : MonoBehaviour
    {
		[SerializeField]
        private List<GameObject> _toSpawnPrefabList = null;

		[SerializeField]
		private Timing _spawnRate = null;

		[SerializeField]
		private List<Transform> _spawnPointsList = new List<Transform>();

		[SerializeField]
		private bool _shouldSpawnOnStart = false;

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

		public void Stop()
		{
			isSpawning = false;
		}

		public void Spawn()
		{
			Transform spawnPoint = _spawnPointsList.Random();
			Instantiate(_toSpawnPrefabList.Random(), spawnPoint);
		}

		private void Start()
		{
			spawnRate.OnReachMax += Spawn;
		}

		private void FixedUpdate()
		{
			spawnRate.Update();
		}
	}
}
