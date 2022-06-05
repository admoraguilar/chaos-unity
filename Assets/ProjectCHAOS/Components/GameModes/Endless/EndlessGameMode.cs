using System.Collections.Generic;
using UnityEngine;
using WaterToolkit;
using WaterToolkit.Drops;
using WaterToolkit.Scores;
using WaterToolkit.Behave;
using WaterToolkit.Worlds;
using WaterToolkit.Weapons;
using WaterToolkit.Pickups;
using WaterToolkit.Spawners;
using WaterToolkit.FlowTrees;
using ProjectCHAOS.UI;
using ProjectCHAOS.Upgrades;
using ProjectCHAOS.GameInputs;
using ProjectCHAOS.Characters.Players;
using ProjectCHAOS.Characters.AIs;
using ProjectCHAOS.GameSerialization;

using URandom = UnityEngine.Random;
using ProjectCHAOS.Waves;

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
		private Upgrader _upgrader = null;

		[SerializeField]
		private PickupHandler _pickupHandler = null;

		[SerializeField]
		private Scorer _scorer = null;
		private Score _score = null;

		[SerializeField]
		private LeanTouchInput _leanTouchInput = null;

		[SerializeField]
		private GlobalUI _globalUi = null;

		[SerializeField]
		private WaveRunner _waveRunner = null;

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
		private SimpleSpawner _spawner = null;

		[Header("Drops")]
		[SerializeField]
		private DropObject _dropsOnAi = null;

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
			_globalUi.upgraderUi.gameObject.SetActive(true);
			_globalUi.touchUiController.gameObject.SetActive(true);

			_waveRunner.BeginStep();
		}

		private void OnGameLeave()
		{
			_upgrader.ResetObjectIndex();
			_upgrader.ResetAll();

			_globalUi.hudUi.gameObject.SetActive(false);
			_globalUi.upgraderUi.gameObject.SetActive(false);
			_globalUi.touchUiController.gameObject.SetActive(false);
		}

		private void OnGameOverVisit()
		{
			_playerCharacter.health.OnHealthEmpty -= OnPlayerCharacterHealthEmpty;

			_waveRunner.EndStep();

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

		private void OnMobFinish()
		{
			Debug.Log("Mob done!!!");
			_waveRunner.EndStep();

			if(_waveRunner.index < _waveRunner.collection.Count) {
				_waveRunner.BeginStep();
			} else {
				Debug.Log("Wave done!!!");
			}
		}

		private void OnWaveBeginStep(WaveData data)
		{
			MobBehaviour mob = data.instance.GetComponent<MobBehaviour>();
			mob.spawner.OnSpawn += OnSpawn;
			mob.OnFinish += OnMobFinish;
			mob.Run();
		}

		private void OnWaveEndStep(WaveData data)
		{
			MobBehaviour mob = data.instance.GetComponent<MobBehaviour>();
			mob.spawner.OnSpawn -= OnSpawn;
			mob.OnFinish -= OnMobFinish;
			mob.Stop();
		}

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
						if(healthAI != null) { healthAI.health.Kill(); }

						// Drop 
						if(_dropsOnAi.CanDrop()) {
							GameObject aiDrop = _dropsOnAi.Get();
							Instantiate(aiDrop, basicAI.transform.position, Quaternion.identity);
						}

						// Destroy bullet
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

			_upgrader.AddUpgradables(new Transform[] {
				_playerCharacter.transform
			});

			_pickupHandler.Initialize(new Transform[] {
				_upgrader.transform
			});

			_globalUi.Initialize(
				_leanTouchInput, _scorer,
				_upgrader);
			
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
			//_spawner.OnSpawn += OnSpawn;
			_globalUi.startMenuUI.OnPressAnywhere += OnStartMenuPressedAnywhere;
			_waveRunner.OnBeginStep += OnWaveBeginStep;
			_waveRunner.OnEndStep += OnWaveEndStep;
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
			//_spawner.OnSpawn -= OnSpawn;
			_globalUi.startMenuUI.OnPressAnywhere -= OnStartMenuPressedAnywhere;
			_waveRunner.OnBeginStep -= OnWaveBeginStep;
			_waveRunner.OnEndStep -= OnWaveEndStep;
		}

		#endregion
	}
}
