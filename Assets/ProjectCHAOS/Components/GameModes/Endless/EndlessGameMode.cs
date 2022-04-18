using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Feedbacks;
using ProjectCHAOS.UI;
using ProjectCHAOS.Inputs;
using ProjectCHAOS.FlowTrees;
using ProjectCHAOS.Scores;
using ProjectCHAOS.Behave;
using ProjectCHAOS.Levels;
using ProjectCHAOS.Weapons;
using ProjectCHAOS.Spawners;
using ProjectCHAOS.GameSerialization;
using ProjectCHAOS.Characters.AIs;
using ProjectCHAOS.Characters.Players;

using UObject = UnityEngine.Object;
using URandom = UnityEngine.Random;

namespace ProjectCHAOS.GameModes.Endless
{
	/// <summary>
	/// Could be split to small sub-classes in order to have better readability.
	/// </summary>
	[DefaultExecutionOrder(-1)]
	public class EndlessGameMode : MonoBehaviour
	{
		[Header("Flow")]
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

		[Space]
		[SerializeField]
		private GameSerializer _gameSerializer = null;

		[SerializeField]
		private Scorer _scorer = null;
		private Score _score = null;

		[SerializeField]
		private LeanTouchInput _leanTouchInput = null;

		[SerializeField]
		private GlobalUI _globalUi = null;

		[Header("World")]
		[SerializeField]
		private Map _map = null;

		[SerializeField]
		private PlayerCharacter _playerCharacter = null;

		[SerializeField]
		private CollisionEvents _playerCollisionEvents = null;

		[SerializeField]
		private LevelArea _levelArea = null;

		[SerializeField]
		private BasicSpawner _spawner = null;

		#region FLOW

		private void OnInitializeVisit()
		{
			_globalUi.HideAllUI();

			_playerCharacter.transform.position = _map.spawnPoints[0].position;
		}

		private void OnStartMenuVisit()
		{
			_globalUi.startMenuUI.gameObject.SetActive(true);
		}

		private void OnStartMenuLeave()
		{
			_globalUi.startMenuUI.gameObject.SetActive(false);
			_score.Reset();
		}

		private void OnGameVisit()
		{
			_playerCharacter.health.OnHealthEmpty += OnPlayerCharacterHealthEmpty;

			_globalUi.hudUi.gameObject.SetActive(true);
			_globalUi.touchUiController.gameObject.SetActive(true);

			_spawner.Run();
		}

		private void OnGameLeave()
		{
			_globalUi.hudUi.gameObject.SetActive(false);
			_globalUi.touchUiController.gameObject.SetActive(false);
		}

		private void OnGameOverVisit()
		{
			_playerCharacter.health.OnHealthEmpty -= OnPlayerCharacterHealthEmpty;

			_spawner.DespawnAll();
			_spawner.Stop();

			_gameSerializer.Save();
			_globalUi.touchUiController.SimulateOnPointerUp();

			_gameOverFlow.Next();
		}

		private void OnReloadVisit()
		{
			_playerCharacter.transform.position = _map.spawnPoints[0].position;
		}

		private void OnPlayerCharacterHealthEmpty()
		{
			_gameFlow.Next();
		}

		private void OnStartMenuPressedAnywhere()
		{
			_startMenuFlow.Next();
		}

		#endregion

		#region EVENTS

		private void OnBulletHitEnemy()
		{
			_score.current += URandom.Range(1, 5);
		}

		private void OnSpawn(GameObject go)
		{
			// Setup AI
			BasicAI basicAI = go.GetComponent<BasicAI>();
			if(basicAI != null) {
				basicAI.Initialize(_levelArea);
			}

			// Get all collision events of enemy prefabs
			List<CollisionEvents> collisionEvents = new List<CollisionEvents>();
			go.GetComponentsInChildren(collisionEvents);

			// Add a score on enemy die
			foreach(CollisionEvents collisionEvent in collisionEvents) {
				collisionEvent.OnTriggerEnterResponse += OnBulletTriggerEnterResponse;

				void OnBulletTriggerEnterResponse(Collider collider)
				{
					if(basicAI != null && collider.gameObject.TryGetComponent(out Bullet bullet)) {
						IHealth healthAI = collisionEvent.gameObject.GetComponentInParentAndChildren<IHealth>();
						if(healthAI != null) {
							healthAI.health.Kill();
						}

						Destroy(bullet.gameObject);
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

		#endregion

		#region MONOBEHAVIOUR

		private void Awake()
		{
			_gameSerializer.Initialize(_scorer, _playerCharacter);
			_gameSerializer.Load();

			_globalUi.Initialize(_leanTouchInput, _scorer);
			
			_score = _scorer.GetScore(0);
		}

		private void OnEnable()
		{
			_initializeFlow.OnVisit += OnInitializeVisit;
			_startMenuFlow.OnVisit += OnStartMenuVisit;
			_startMenuFlow.OnLeave += OnStartMenuLeave;
			_gameFlow.OnVisit += OnGameVisit;
			_gameFlow.OnLeave += OnGameLeave;
			_gameOverFlow.OnVisit += OnGameOverVisit;
			_reloadFlow.OnVisit += OnReloadVisit;

			_playerCollisionEvents.OnCollisionEnterResponse += OnPlayerCollisionEnter;
			_spawner.OnSpawn += OnSpawn;
			_globalUi.startMenuUI.OnPressAnywhere += OnStartMenuPressedAnywhere;
		}

		private void OnDisable()
		{
			_initializeFlow.OnVisit -= OnInitializeVisit;
			_startMenuFlow.OnVisit -= OnStartMenuVisit;
			_startMenuFlow.OnLeave -= OnStartMenuLeave;
			_gameFlow.OnVisit -= OnGameVisit;
			_gameFlow.OnLeave -= OnGameLeave;
			_gameOverFlow.OnVisit -= OnGameOverVisit;
			_reloadFlow.OnVisit -= OnReloadVisit;

			_playerCollisionEvents.OnCollisionEnterResponse -= OnPlayerCollisionEnter;
			_spawner.OnSpawn -= OnSpawn;
			_globalUi.startMenuUI.OnPressAnywhere -= OnStartMenuPressedAnywhere;
		}

		#endregion
	}
}
