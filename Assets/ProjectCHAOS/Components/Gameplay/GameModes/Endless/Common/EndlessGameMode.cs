using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Feedbacks;
using ProjectCHAOS.Systems.FlowTrees;
using ProjectCHAOS.Systems.Inputs;
using ProjectCHAOS.Gameplay.Menus;
using ProjectCHAOS.Gameplay.Behave;
using ProjectCHAOS.Gameplay.Scores;
using ProjectCHAOS.Gameplay.Levels;
using ProjectCHAOS.Gameplay.Weapons;
using ProjectCHAOS.Gameplay.Spawners;
using ProjectCHAOS.Gameplay.Characters.AIs;

namespace ProjectCHAOS.Gameplay.GameModes
{
	/// <summary>
	/// Could be split to small sub-classes in order to have better readability.
	/// </summary>
	[DefaultExecutionOrder(-1)]
	public class EndlessGameMode : MonoBehaviour
	{
		[Header("Systems")]
		[SerializeField]
		private Scorer _scorer = null;
		private Score _score = null;

		[SerializeField]
		private LeanTouchInput _leanTouchInput = null;

		[Header("Game States")]
		[SerializeField]
		private Node _initializeFlow = null;
		
		[SerializeField]
		private Node _startMenuFlow = null;

		[SerializeField]
		private Node _gameFlow = null;

		[SerializeField]
		private Node _gameOverFlow = null;

		[Header("Gameplay")]
		[SerializeField]
		private CollisionEvents _playerCollisionEvents = null;

		[SerializeField]
		private LevelArea _levelArea = null;

		[SerializeField]
		private BasicSpawner _spawner = null;

		[Header("UIs")]
		[SerializeField]
		private DynamicJoystick _joystick = null;

		[SerializeField]
		private TouchUIController _touchUi = null;

		[SerializeField]
		private StartMenuUI _startMenuUi = null;

		[SerializeField]
		private ScoreUI _hudScoreUi = null;

		private void OnStartMenuTouchScreen()
		{
			_score.Reset();
			_startMenuFlow.Next();
		}

		private void OnPlayerDies(Collision collision)
		{
			GameObject go = collision.gameObject;
			if(!go.CompareTag("Enemy")) { return; }

			//_game.Next();
			//_joystick.OnPointerUp(new PointerEventData(EventSystem.current));
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
					if(collider.gameObject.TryGetComponent(out Bullet bullet)) {
						//Destroy(collisionEvent.gameObject);
						MMFeedbacks feedbacks = collisionEvent.gameObject.GetComponentInChildren<MMFeedbacks>();
						if(feedbacks != null && !feedbacks.IsPlaying) {
							feedbacks.PlayFeedbacks();
						}

						Destroy(bullet.gameObject);

						_score.current += Random.Range(1, 5);
					}
				}
			}
		}

		private void OnGameFlowVisit()
		{
			_spawner.Run();
		}

		private void OnGameOverFlowVisit()
		{
			_spawner.DespawnAll();
		}

		private void Awake()
		{
			_touchUi.Initialize(_leanTouchInput);
			_startMenuUi.Initialize(_leanTouchInput, _scorer);
			_hudScoreUi.Initialize(_scorer);

			_score = _scorer.GetScore(0);
		}

		private void OnEnable()
		{
			_startMenuUi.OnTouchScreen += OnStartMenuTouchScreen;
			_playerCollisionEvents.OnCollisionEnterResponse += OnPlayerDies;

			_spawner.OnSpawn += OnSpawn;

			_gameFlow.OnVisit += OnGameFlowVisit;
			_gameOverFlow.OnVisit += OnGameOverFlowVisit;
		}

		private void OnDisable()
		{
			_startMenuUi.OnTouchScreen -= OnStartMenuTouchScreen;
			_playerCollisionEvents.OnCollisionEnterResponse -= OnPlayerDies;

			_spawner.OnSpawn -= OnSpawn;

			_gameFlow.OnVisit -= OnGameFlowVisit;
			_gameOverFlow.OnVisit -= OnGameOverFlowVisit;
		}
	}
}
