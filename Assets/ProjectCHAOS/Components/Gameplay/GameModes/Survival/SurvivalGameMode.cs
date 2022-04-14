using ProjectCHAOS.Gameplay.Characters;
using ProjectCHAOS.Systems.Inputs;
using ProjectCHAOS.Gameplay.Levels;
using UnityEngine;
using ProjectCHAOS.Systems.FlowTrees;
using ProjectCHAOS.Gameplay.Scores;
using ProjectCHAOS.Gameplay.Characters.Players;

namespace ProjectCHAOS.Gameplay.GameModes
{
	[DefaultExecutionOrder(-1)]
	public class SurvivalGameMode : MonoBehaviour
	{
		[Header("Systems")]
		[SerializeField]
		private FlowTree _flowTree = null;

		[Header("Nodes")]
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
		private LeanTouchInput _leanTouchInput = null;

		[SerializeField]
		private Scorer _scorer = null;

		[Header("UIs")]
		[SerializeField]
		private TouchUIController _touchUiController = null;

		[Header("Gameplay")]
		[SerializeField]
		private Map _baseMap = null;

		[SerializeField]
		private Portal _basePortal = null;

		[SerializeField]
		private Map _outsideMap = null;

		[SerializeField]
		private Portal _outsidePortal = null;

		[SerializeField]
		private CharacterMechanic _character = null;

		[SerializeField]
		private PlayerCharacter _playerCharacter = null;

		private void OnBasePortalEnter(GameObject onEnter)
		{
			CharacterMechanic character = onEnter.GetComponentInParent<CharacterMechanic>();
			if(character != null) { _base.Next(); }
		}

		private void OnOutsidePortalEnter(GameObject onEnter)
		{
			CharacterMechanic character = onEnter.GetComponentInParent<CharacterMechanic>();
			if(character != null) {
				character.transform.position = _baseMap.spawnPoints[0].position;
			}
		}

		private void OnBaseNodeVisit()
		{

		}

		private void OnOutsideVisit()
		{
			_playerCharacter.health.OnHealthEmpty += OnPlayerCharacterHealthEmpty;
			
			_character.transform.position = _outsideMap.spawnPoints[0].position;
		}

		private void OnOutsideDeadVisit()
		{
			Debug.Log("Outside dead..");

			_playerCharacter.health.OnHealthEmpty -= OnPlayerCharacterHealthEmpty;

			// Do some transition here...
			_character.transform.position = _baseMap.spawnPoints[0].position;

			_outsideDead.Next();
		}

		private void OnReloadVisit()
		{
			_playerCharacter.health.Restore();

			Debug.Log("Reloading..");
		}

		private void OnPlayerCharacterHealthEmpty()
		{
			_outside.Next();
		}

		private void OnEnable()
		{
			_basePortal.OnEnter += OnBasePortalEnter;
			_outsidePortal.OnEnter += OnOutsidePortalEnter;

			_base.OnVisit += OnBaseNodeVisit;
			_outside.OnVisit += OnOutsideVisit;
			_outsideDead.OnVisit += OnOutsideDeadVisit;
			_reload.OnVisit += OnReloadVisit;
		}

		private void OnDisable()
		{
			_basePortal.OnEnter -= OnBasePortalEnter;
			_outsidePortal.OnEnter -= OnOutsidePortalEnter;

			_base.OnVisit -= OnBaseNodeVisit;
			_outside.OnVisit -= OnOutsideVisit;
			_outsideDead.OnVisit -= OnOutsideDeadVisit;
			_reload.OnVisit -= OnReloadVisit;
		}

		private void Start()
		{
			_touchUiController.Initialize(_leanTouchInput);
		}
	}
}
