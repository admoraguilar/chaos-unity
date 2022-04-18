using System.Collections.Generic;
using UnityEngine;
using ProjectCHAOS.Spawners;

namespace ProjectCHAOS.Levels
{
	public class Map : MonoBehaviour
	{
		[SerializeField]
		private List<Collider> _bounds = new List<Collider>();
		
		[SerializeField]
		private List<Killzone> _killzones = new List<Killzone>();
		
		[SerializeField]
		private List<Transform> _spawnPoints = new List<Transform>();

		public List<Collider> bounds => _bounds;

		public List<Killzone> killzones => _killzones;
		
		public List<Transform> spawnPoints => _spawnPoints;
	}
}
