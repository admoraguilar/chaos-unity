using System;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Feedbacks;
using ProjectCHAOS.Systems;
using ProjectCHAOS.Gameplay.Behave;
using ProjectCHAOS.Gameplay.Levels;
using ProjectCHAOS.Gameplay.Weapons;
using ProjectCHAOS.Gameplay.Spawners;
using ProjectCHAOS.Gameplay.Characters.AIs;
using ProjectCHAOS.Gameplay.Characters.Players;

using UObject = UnityEngine.Object;

namespace ProjectCHAOS.Gameplay.GameModes
{
	[Serializable]
	public class EndlessWorld : GameWorld
	{
		public event Action OnBulletHitEnemy = delegate { };
		public event Action OnPlayerCharacterHealthEmpty = delegate { };

		[SerializeField]
		private PlayerCharacter _playerCharacter = null;

		[SerializeField]
		private CollisionEvents _playerCollisionEvents = null;

		[SerializeField]
		private LevelArea _levelArea = null;

		[SerializeField]
		private BasicSpawner _spawner = null;

		public void OnGameVisit()
		{
			_playerCharacter.health.OnHealthEmpty += OnPlayerCharacterHealthEmptyInvoke;
			_spawner.Run();
		}

		public void OnGameOverVisit()
		{
			_playerCharacter.health.OnHealthEmpty -= OnPlayerCharacterHealthEmptyInvoke;
			_spawner.DespawnAll();
			_spawner.Stop();
		}

		private void OnSpawn(GameObject go)
		{
			BasicAI basicAI = go.GetComponent<BasicAI>();
			if(basicAI != null) {
				basicAI.Initialize(_levelArea);
			}

			// Get all collision events of enemy prefab
			List<CollisionEvents> collisionEvents = new List<CollisionEvents>();
			go.GetComponentsInChildren(collisionEvents);

			// Add a score on enemy die
			foreach(CollisionEvents collisionEvent in collisionEvents) {
				collisionEvent.OnTriggerEnterResponse += OnBulletTriggerEnterResponse;

				void OnBulletTriggerEnterResponse(Collider collider)
				{
					if(basicAI != null && collider.gameObject.TryGetComponent(out Bullet bullet)) {
						MMFeedbacks feedbacks = collisionEvent.gameObject.GetComponentInChildren<MMFeedbacks>();
						if(feedbacks != null && !feedbacks.IsPlaying) {
							feedbacks.PlayFeedbacks();
						}

						UObject.Destroy(bullet.gameObject);
						OnBulletHitEnemy();
					}
				}
			}
		}

		private void OnPlayerCollisionEnter(Collision collision)
		{
			BasicAI basicAI = collision.gameObject.GetComponentInParentAndChildren<BasicAI>();
			if(basicAI != null) {
				_playerCharacter.health.Kill();
			}
		}

		private void OnPlayerCharacterHealthEmptyInvoke()
		{
			OnPlayerCharacterHealthEmpty();
		}

		protected override void OnDoEnable()
		{
			_playerCollisionEvents.OnCollisionEnterResponse += OnPlayerCollisionEnter;
			_spawner.OnSpawn += OnSpawn;
		}

		protected override void OnDoDisable()
		{
			_playerCollisionEvents.OnCollisionEnterResponse -= OnPlayerCollisionEnter;
			_spawner.OnSpawn -= OnSpawn;
		}
	}
}
