using System;
using UnityEngine;
using ProjectCHAOS.Systems.FlowTrees;

namespace ProjectCHAOS.Gameplay.GameModes.Endless
{
	[Serializable]
	public class EndlessFlow : GameFlow<EndlessWorld, EndlessSystem>
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

		private void OnInitializeVisit()
		{
			world.OnInitializeVisit();
			system.OnInitializeVisit();
		}

		private void OnStartMenuVisit()
		{
			system.OnStartMenuVisit();
		}

		private void OnStartMenuLeave()
		{
			system.OnStartMenuLeave();
		}

		private void OnGameVisit()
		{
			world.OnGameVisit();
			system.OnGameVisit();
		}

		private void OnGameLeave()
		{
			system.OnGameLeave();
		}

		private void OnGameOverVisit()
		{
			system.OnGameOverVisit();
			world.OnGameOverVisit();
			_gameOverFlow.Next();
		}

		private void OnReloadVisit()
		{
			world.OnReloadVisit();
		}

		private void OnStartMenuPressAnywhere()
		{
			_startMenuFlow.Next();
		}

		private void OnPlayerCharacterHealthEmpty()
		{
			_gameFlow.Next();
		}

		protected override void OnDoEnable()
		{
			_initializeFlow.OnVisit += OnInitializeVisit;
			_startMenuFlow.OnVisit += OnStartMenuVisit;
			_startMenuFlow.OnLeave += OnStartMenuLeave;
			_gameFlow.OnVisit += OnGameVisit;
			_gameFlow.OnLeave += OnGameLeave;
			_gameOverFlow.OnVisit += OnGameOverVisit;
			_reloadFlow.OnVisit += OnReloadVisit;

			system.OnStartMenuPressAnywhere += OnStartMenuPressAnywhere;
			world.OnPlayerCharacterHealthEmpty += OnPlayerCharacterHealthEmpty;
		}

		protected override void OnDoDisable()
		{
			_initializeFlow.OnVisit -= OnInitializeVisit;
			_startMenuFlow.OnVisit -= OnStartMenuVisit;
			_startMenuFlow.OnLeave -= OnStartMenuLeave;
			_gameFlow.OnVisit -= OnGameVisit;
			_gameFlow.OnLeave -= OnGameLeave;
			_gameOverFlow.OnVisit -= OnGameOverVisit;
			_reloadFlow.OnVisit -= OnReloadVisit;

			system.OnStartMenuPressAnywhere -= OnStartMenuPressAnywhere;
			world.OnPlayerCharacterHealthEmpty -= OnPlayerCharacterHealthEmpty;
		}
	}
}
