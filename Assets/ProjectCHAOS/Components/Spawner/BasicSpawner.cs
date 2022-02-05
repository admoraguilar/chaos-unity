using System.Collections.Generic;
using UnityEngine;
using ProjectCHAOS.Behave;

namespace ProjectCHAOS.Spawners
{
    public class BasicSpawner : MonoBehaviour
    {
		[SerializeField]
        private GameObject _toSpawnPrefab = null;

		[SerializeField]
		private Timing _spawnRate = null;

		[SerializeField]
		private List<Transform> _spawnPoints = new List<Transform>();

		private float spawnTimer = 0f;

		public GameObject toSpawnPrefab
		{
			get => _toSpawnPrefab;
			set => _toSpawnPrefab = value;
		}

		public Timing spawnRate => _spawnRate;

		public List<Transform> spawnPoints => _spawnPoints;

		private void Spawn()
		{
			int randomSpawnPointIndex = Random.Range(0, _spawnPoints.Count);
			Transform spawnPoint = _spawnPoints[randomSpawnPointIndex];
			Instantiate(_toSpawnPrefab, spawnPoint);
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
