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
		private List<Transform> _spawnPoints = new List<Transform>();

		public List<GameObject> toSpawnPrefabList
		{
			get => _toSpawnPrefabList;
			set => _toSpawnPrefabList = value;
		}

		public Timing spawnRate => _spawnRate;

		public List<Transform> spawnPoints => _spawnPoints;

		private void Spawn()
		{
			int randomSpawnPointIndex = Random.Range(0, _spawnPoints.Count);
			Transform spawnPoint = _spawnPoints[randomSpawnPointIndex];
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
