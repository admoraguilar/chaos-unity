using System.Collections.Generic;
using UnityEngine;

namespace ProjectCHAOS.Rules
{
    public class Spawner : MonoBehaviour
    {
        public GameObject toSpawnPrefab = null;
        public float spawnRate = .3f;
        public List<Transform> spawnPoints = new List<Transform>();

		private float spawnTimer = 0f;

		private void FixedUpdate()
		{
			spawnTimer += Time.deltaTime;
			if(spawnTimer > spawnRate) {
				int randomSpawnPointIndex = Random.Range(0, spawnPoints.Count);
				Transform spawnPoint = spawnPoints[randomSpawnPointIndex];
				Instantiate(toSpawnPrefab, spawnPoint);
				spawnTimer = 0f;
			}
		}
	}
}
