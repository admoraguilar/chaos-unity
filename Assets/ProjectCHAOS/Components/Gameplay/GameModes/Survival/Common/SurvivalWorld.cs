using System;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Feedbacks;
using ProjectCHAOS.Gameplay.Behave;
using ProjectCHAOS.Gameplay.Levels;
using ProjectCHAOS.Gameplay.Weapons;
using ProjectCHAOS.Gameplay.Spawners;
using ProjectCHAOS.Gameplay.Characters;
using ProjectCHAOS.Gameplay.Characters.Players;
using ProjectCHAOS.Gameplay.Characters.AIs;

using UObject = UnityEngine.Object;

namespace ProjectCHAOS.Gameplay.GameModes.Survival
{
	[Serializable]
	public class SurvivalWorld
	{
		public event Action<CharacterMechanic> OnCharacterBasePortalEnter = delegate { };
		public event Action<GameObject> OnBulletHit = delegate { };
		public event Action OnPlayerCharacterHealthEmpty = delegate { };

		[Header("Base")]
		[SerializeField]
		private Map _baseMap = null;

		[SerializeField]
		private Portal _basePortal = null;

		[Header("Outside")]
		[SerializeField]
		private Map _outsideMap = null;

		[SerializeField]
		private Portal _outsidePortal = null;

		[SerializeField]
		private BasicSpawner _spawner = null;

		[SerializeField]
		private LevelArea _outsideLevelArea = null;

		[Header("All")]
		[SerializeField]
		private CharacterMechanic _character = null;

		[SerializeField]
		private PlayerCharacter _playerCharacter = null;

		public Map baseMap => _baseMap;

		public Portal basePortal => _basePortal;

		public Map outsideMap => _outsideMap;

		public Portal outsidePortal => _outsidePortal;

		public BasicSpawner spawner => _spawner;

		public LevelArea outsideLevelArea => _outsideLevelArea;

		public CharacterMechanic character => _character;

		public PlayerCharacter playerCharacter => _playerCharacter;

		public void OnOutsideVisit()
		{
			playerCharacter.health.OnHealthEmpty += OnHealthEmpty;
			character.transform.position = outsideMap.spawnPoints[0].position;
			spawner.Run();
		}

		public void OnOutsideDeadVisit()
		{
			playerCharacter.health.OnHealthEmpty -= OnHealthEmpty;
			character.transform.position = baseMap.spawnPoints[0].position;
			spawner.Stop();
		}

		public void OnReloadVisit()
		{
			playerCharacter.health.Restore();
		}

		private void OnBasePortalEnter(GameObject onEnter)
		{
			CharacterMechanic character = onEnter.GetComponentInParent<CharacterMechanic>();
			if(character != null) { OnCharacterBasePortalEnter(character); }
		}

		private void OnOutsidePortalEnter(GameObject onEnter)
		{
			CharacterMechanic character = onEnter.GetComponentInParent<CharacterMechanic>();
			if(character != null) {
				character.transform.position = _baseMap.spawnPoints[0].position;
			}
		}

		private void OnSpawn(GameObject obj)
		{
			BasicAI basicAI = obj.GetComponent<BasicAI>();
			if(basicAI != null) {
				basicAI.Initialize(_outsideLevelArea);
			}

			// Get all collision events of enemy prefab
			List<CollisionEvents> collisionEvents = new List<CollisionEvents>();
			obj.GetComponentsInChildren(collisionEvents);

			// Add a score on enemy die
			foreach(CollisionEvents collisionEvent in collisionEvents) {
				collisionEvent.OnTriggerEnterResponse += OnBulletCollisionResponse;

				void OnBulletCollisionResponse(Collider collider)
				{
					if(collider.gameObject.TryGetComponent(out Bullet bullet)) {
						//Destroy(collisionEvent.gameObject);
						MMFeedbacks feedbacks = collisionEvent.gameObject.GetComponentInChildren<MMFeedbacks>();
						if(feedbacks != null && !feedbacks.IsPlaying) {
							feedbacks.PlayFeedbacks();
						}

						UObject.Destroy(bullet.gameObject);
						OnBulletHit(obj);
					}
				}
			}
		}

		private void OnHealthEmpty()
		{
			OnPlayerCharacterHealthEmpty();
		}

		public void OnEnable()
		{
			_basePortal.OnEnter += OnBasePortalEnter;
			_outsidePortal.OnEnter += OnOutsidePortalEnter;
			_spawner.OnSpawn += OnSpawn;
		}

		public void OnDisable()
		{
			_basePortal.OnEnter -= OnBasePortalEnter;
			_outsidePortal.OnEnter -= OnOutsidePortalEnter;
			_spawner.OnSpawn -= OnSpawn;
		}
	}
}