using System;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Feedbacks;
using ProjectCHAOS.UI;
using ProjectCHAOS.Systems.FlowTrees;
using ProjectCHAOS.Systems.Inputs;
using ProjectCHAOS.Gameplay.Behave;
using ProjectCHAOS.Gameplay.Scores;
using ProjectCHAOS.Gameplay.Levels;
using ProjectCHAOS.Gameplay.Weapons;
using ProjectCHAOS.Gameplay.Spawners;
using ProjectCHAOS.Gameplay.Characters.AIs;

using UObject = UnityEngine.Object;
using ProjectCHAOS.Gameplay.Characters.Players;

namespace ProjectCHAOS.Gameplay.GameModes
{
	/// <summary>
	/// Could be split to small sub-classes in order to have better readability.
	/// </summary>
	[DefaultExecutionOrder(-1)]
	public class EndlessGameMode : MonoBehaviour
	{
		[SerializeField]
		private EndlessSystem _system = null;
		
		[SerializeField]
		private EndlessWorld _world = null;
		
		[SerializeField]
		private EndlessFlow _flow = null;

		public EndlessSystem system => _system;
		public EndlessWorld world => _world;
		public EndlessFlow flow => _flow;

		private void Awake()
		{
			_flow.Awake(this);
		}

		private void OnEnable()
		{
			_flow.OnEnable();
		}

		private void OnDisable()
		{
			_flow.OnDisable();
		}
	}

	[Serializable]
	public class EndlessFlow
	{
		[SerializeField]
		private Node _initializeFlow = null;

		[SerializeField]
		private Node _startMenuFlow = null;

		[SerializeField]
		private Node _gameFlow = null;

		[SerializeField]
		private Node _gameOverFlow = null;

		[SerializeField]
		private Node _reloadFlow = null;

		private EndlessGameMode _gameMode = null;

		private void OnInitializeVisit()
		{
			_gameMode.system.OnInitializeVisit();
		}

		private void OnStartMenuVisit()
		{
			_gameMode.system.OnStartMenuVisit();
		}

		private void OnStartMenuLeave()
		{
			_gameMode.system.OnStartMenuLeave();
		}

		private void OnGameVisit()
		{
			_gameMode.system.OnGameVisit();
			_gameMode.world.OnGameVisit();
		}

		private void OnGameLeave()
		{
			_gameMode.system.OnGameLeave();
		}

		private void OnGameOverVisit()
		{
			_gameMode.world.OnGameOverVisit();
			_gameOverFlow.Next();
		}

		private void OnStartMenuPressAnywhere()
		{
			_startMenuFlow.Next();
		}

		private void OnPlayerCharacterHealthEmpty()
		{
			_gameFlow.Next();
		}

		public void Awake(EndlessGameMode gameMode)
		{
			_gameMode = gameMode;

			_gameMode.system.Awake();
		}

		public void OnEnable()
		{
			_initializeFlow.OnVisit += OnInitializeVisit;
			_startMenuFlow.OnVisit += OnStartMenuVisit;
			_startMenuFlow.OnLeave += OnStartMenuLeave;
			_gameFlow.OnVisit += OnGameVisit;
			_gameFlow.OnLeave += OnGameLeave;
			_gameOverFlow.OnVisit += OnGameOverVisit;

			_gameMode.system.OnStartMenuPressAnywhere += OnStartMenuPressAnywhere;
			_gameMode.world.OnPlayerCharacterHealthEmpty += OnPlayerCharacterHealthEmpty;

			_gameMode.system.OnEnable();
			_gameMode.world.OnEnable();
		}

		public void OnDisable()
		{
			_initializeFlow.OnVisit -= OnInitializeVisit;
			_startMenuFlow.OnVisit -= OnStartMenuVisit;
			_startMenuFlow.OnLeave -= OnStartMenuLeave;
			_gameFlow.OnVisit -= OnGameVisit;
			_gameFlow.OnLeave -= OnGameLeave;
			_gameOverFlow.OnVisit -= OnGameOverVisit;

			_gameMode.system.OnStartMenuPressAnywhere -= OnStartMenuPressAnywhere;
			_gameMode.world.OnPlayerCharacterHealthEmpty -= OnPlayerCharacterHealthEmpty;

			_gameMode.system.OnDisable();
			_gameMode.world.OnDisable();
		}
	}

	[Serializable]
	public class EndlessSystem
	{
		public event Action OnStartMenuPressAnywhere = delegate { };

		[SerializeField]
		private Scorer _scorer = null;
		private Score _score = null;

		[SerializeField]
		private LeanTouchInput _leanTouchInput = null;

		[SerializeField]
		private GlobalUI _globalUi = null;

		public void OnInitializeVisit()
		{
			_globalUi.HideAllUI();
		}

		public void OnStartMenuVisit()
		{
			_globalUi.startMenuUI.gameObject.SetActive(true);
		}

		public void OnStartMenuLeave()
		{
			_globalUi.startMenuUI.gameObject.SetActive(false);
			_score.Reset();
		}

		public void OnGameVisit()
		{
			_globalUi.hudUi.gameObject.SetActive(true);
			_globalUi.touchUiController.gameObject.SetActive(true);
		}

		public void OnGameLeave()
		{
			_globalUi.hudUi.gameObject.SetActive(false);
			_globalUi.touchUiController.gameObject.SetActive(false);
		}

		public void OnGameOverVisit()
		{

		}

		private void OnStartMenuPressedAnywhereMethod()
		{
			OnStartMenuPressAnywhere();
		}

		public void Awake()
		{
			_globalUi.Initialize(_leanTouchInput, _scorer);

			_score = _scorer.GetScore(0);
		}

		public void OnEnable()
		{
			_globalUi.startMenuUI.OnPressAnywhere += OnStartMenuPressedAnywhereMethod;
		}

		public void OnDisable()
		{
			_globalUi.startMenuUI.OnPressAnywhere -= OnStartMenuPressedAnywhereMethod;
		}
	}
	
	[Serializable]
	public class EndlessWorld
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

						//_score.current += Random.Range(1, 5);
					}
				}
			}
		}

		private void OnPlayerCollisionEnter(Collision collision)
		{
			BasicAI basicAI = collision.gameObject.GetComponentInParent<BasicAI>();
			if(basicAI != null) {
				_playerCharacter.health.Kill();
			}
		}

		private void OnPlayerCharacterHealthEmptyInvoke()
		{
			OnPlayerCharacterHealthEmpty();
		}

		public void OnEnable()
		{
			_playerCollisionEvents.OnCollisionEnterResponse += OnPlayerCollisionEnter;
			_spawner.OnSpawn += OnSpawn;
		}

		public void OnDisable()
		{
			_playerCollisionEvents.OnCollisionEnterResponse -= OnPlayerCollisionEnter;
			_spawner.OnSpawn -= OnSpawn;
		}
	}
}
