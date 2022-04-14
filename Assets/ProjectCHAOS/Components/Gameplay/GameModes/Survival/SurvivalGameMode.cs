using ProjectCHAOS.Gameplay.Characters;
using ProjectCHAOS.Systems.Inputs;
using ProjectCHAOS.Gameplay.Levels;
using ProjectCHAOS.Gameplay.Scores;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectCHAOS.Gameplay.GameModes
{
	[DefaultExecutionOrder(-1)]
	public class SurvivalGameMode : MonoBehaviour
	{
		[Header("Systems")]
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

		private void OnBasePortalEnter(GameObject onEnter)
		{
			CharacterMechanic character = onEnter.GetComponentInParent<CharacterMechanic>();
			if(character != null) {
				character.transform.position = _outsideMap.spawnPoints[0].position;
			}
		}

		private void OnOutsidePortalEnter(GameObject onEnter)
		{
			CharacterMechanic character = onEnter.GetComponentInParent<CharacterMechanic>();
			if(character != null) {
				character.transform.position = _baseMap.spawnPoints[0].position;
			}
		}

		private void OnEnable()
		{
			_basePortal.OnEnter += OnBasePortalEnter;
			_outsidePortal.OnEnter += OnOutsidePortalEnter;
		}

		private void OnDisable()
		{
			_basePortal.OnEnter -= OnBasePortalEnter;
			_outsidePortal.OnEnter += OnOutsidePortalEnter;
		}

		private void Start()
		{
			_touchUiController.Initialize(_leanTouchInput);
		}
	}
}
