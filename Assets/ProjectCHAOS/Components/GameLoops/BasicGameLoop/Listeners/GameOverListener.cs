using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProjectCHAOS.Spawners;

namespace ProjectCHAOS.GameLoops
{
	public class GameOverListener : GameLoopListener
	{
		[SerializeField]
		private BasicSpawner _spawner = null;

		protected override void OnVisit()
		{
			_spawner.DespawnAll();
		}
	}
}
