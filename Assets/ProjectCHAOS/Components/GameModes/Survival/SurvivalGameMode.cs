using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Feedbacks;
using WaterToolkit;
using WaterToolkit.Scores;
using WaterToolkit.Behave;
using WaterToolkit.Worlds;
using WaterToolkit.Weapons;
using WaterToolkit.Spawners;
using WaterToolkit.FlowTrees;
using WaterToolkit.Scoreboards;
using ProjectCHAOS.UI;
using ProjectCHAOS.GameInputs;
using ProjectCHAOS.Characters.AIs;
using ProjectCHAOS.Characters.Players;

using UObject = UnityEngine.Object;
using URandom = UnityEngine.Random;

namespace ProjectCHAOS.GameModes.Survival
{
	[DefaultExecutionOrder(-1)]
	public class SurvivalGameMode : MonoBehaviour
	{
		[SerializeField]
		private Node _base = null;

		[SerializeField]
		private Node _outside = null;

		[SerializeField]
		private Node _outsideDead = null;

		[SerializeField]
		private Node _reload = null;

		[Space]
		[SerializeField]
		private LeanTouchInput _input = null;

		[SerializeField]
		private Scorer _scorer = null;
		private Score _score = null;

		[Header("UIs")]
		[SerializeField]
		private GlobalUI _globalUi = null;

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
		private PlayerCharacter _playerCharacter = null;

		[SerializeField]
		private CollisionEvents _playerCollisionEvents = null;

		#region FLOW

		private void OnBaseVisit()
		{

		}

		private void OnOutsideVisit()
		{
			_globalUi.hudUi.gameObject.SetActive(true);

			_playerCharacter.health.OnHealthEmpty += OnPlayerCharacterHealthEmpty;
			_playerCharacter.transform.position = _outsideMap.spawnPoints[0].position;
			_spawner.Run();
		}

		private void OnOutsideDeadVisit()
		{
			_spawner.Stop();

			_globalUi.hudUi.gameObject.SetActive(false);

			_globalUi.scoreboardUi.OnBackButtonPressed += OnScoreboardUiBackButtonPressed;

			ScoreObject scoreObj = new ScoreObject();
			scoreObj.name = "Test";
			scoreObj.score = _score.current;
			scoreObj.days = 1;
			scoreObj.waves = 1;

			_globalUi.scoreboardUi.Populate(new ScoreObject[] { scoreObj, scoreObj });
			_globalUi.scoreboardUi.gameObject.SetActive(true);

			_outsideDead.Next();

			void OnScoreboardUiBackButtonPressed()
			{
				_globalUi.scoreboardUi.gameObject.SetActive(false);
				_score.Reset();
				_globalUi.scoreboardUi.OnBackButtonPressed -= OnScoreboardUiBackButtonPressed;
			}
		}

		private void OnReloadVisit()
		{
			_playerCharacter.health.OnHealthEmpty -= OnPlayerCharacterHealthEmpty;
			_playerCharacter.transform.position = _baseMap.spawnPoints[0].position;
			_playerCharacter.health.Restore();
		}

		private void OnCharacterBasePortalEnter(PlayerCharacter character)
		{
			_base.Next();
		}

		private void OnPlayerCharacterHealthEmpty()
		{
			_outside.Next();
		}

		#endregion

		#region EVENTS

		private void OnBulletHit(GameObject obj)
		{
			_score.current += URandom.Range(1, 5);
		}

		private void OnBasePortalEnter(GameObject onEnter)
		{
			PlayerCharacter character = onEnter.GetComponentInParent<PlayerCharacter>();
			if(character != null) { OnCharacterBasePortalEnter(character); }
		}

		private void OnOutsidePortalEnter(GameObject onEnter)
		{
			PlayerCharacter character = onEnter.GetComponentInParent<PlayerCharacter>();
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

						Destroy(bullet.gameObject);
						OnBulletHit(obj);
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
			_score = _scorer.GetScore(0);
			_globalUi.startMenuUI.Initialize(_input, _scorer);
			_globalUi.hudUi.scoreUi.Initialize(_scorer);

			_globalUi.touchUiController.Initialize(_input);
		}

		private void OnEnable()
		{
			_base.OnVisit += OnBaseVisit;
			_outside.OnVisit += OnOutsideVisit;
			_outsideDead.OnVisit += OnOutsideDeadVisit;
			_reload.OnVisit += OnReloadVisit;

			_basePortal.OnEnter += OnBasePortalEnter;
			_outsidePortal.OnEnter += OnOutsidePortalEnter;
			_spawner.OnSpawn += OnSpawn;

			_playerCollisionEvents.OnCollisionEnterResponse += OnPlayerCollisionEnter;
		}

		private void OnDisable()
		{
			_base.OnVisit -= OnBaseVisit;
			_outside.OnVisit -= OnOutsideVisit;
			_outsideDead.OnVisit -= OnOutsideDeadVisit;
			_reload.OnVisit -= OnReloadVisit;

			_basePortal.OnEnter -= OnBasePortalEnter;
			_outsidePortal.OnEnter -= OnOutsidePortalEnter;
			_spawner.OnSpawn -= OnSpawn;

			_playerCollisionEvents.OnCollisionEnterResponse -= OnPlayerCollisionEnter;
		}

		#endregion
	}
}
