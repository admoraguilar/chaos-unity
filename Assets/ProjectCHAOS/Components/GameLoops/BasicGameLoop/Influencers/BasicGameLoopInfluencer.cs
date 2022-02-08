using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Feedbacks;
using ProjectCHAOS.Common;
using ProjectCHAOS.Behave;
using ProjectCHAOS.Scores;
using ProjectCHAOS.Spawners;
using ProjectCHAOS.Weapons;
using ProjectCHAOS.GUI.Menus;

namespace ProjectCHAOS.GameLoops
{
	public class BasicGameLoopInfluencer : MonoBehaviour
	{
		[SerializeField]
		private CollisionEvents _playerCollisionEvents = null;

		[SerializeField]
		private BasicSpawner _spawner = null;

		[Space]
		[SerializeField]
		private StartMenuUI _startMenuUI = null;

		[SerializeField]
		private DynamicJoystick _joystick = null;

		[Space]
		[SerializeField]
		private Node _startMenu = null;

		[SerializeField]
		private Node _game = null;

		private Score _score = null;

		private void OnStartMenuTouchScreen()
		{
			_score.Reset();
			_startMenu.Next();
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
			// Get all collision events of enemy prefab
			List<CollisionEvents> collisionEvents = new List<CollisionEvents>();
			go.GetComponentsInChildren(collisionEvents);

			// Add a score on enemy die
			foreach(CollisionEvents collisionEvent in collisionEvents) {
				collisionEvent.OnCollisionEnterResponse += OnBulletCollisionResponse;

				void OnBulletCollisionResponse(Collision collision)
				{
					if(collision.gameObject.TryGetComponent(out Bullet bullet)) {
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

		private void Awake()
		{
			_score = Scorer.Instance.GetScore(0);
		}

		private void OnEnable()
		{
			_startMenuUI.OnTouchScreen += OnStartMenuTouchScreen;
			_playerCollisionEvents.OnCollisionEnterResponse += OnPlayerDies;

			_spawner.OnSpawn += OnSpawn;
		}

		private void OnDisable()
		{
			_startMenuUI.OnTouchScreen -= OnStartMenuTouchScreen;
			_playerCollisionEvents.OnCollisionEnterResponse -= OnPlayerDies;

			_spawner.OnSpawn -= OnSpawn;
		}
	}
}
