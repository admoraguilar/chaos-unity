using UnityEngine;
using ProjectCHAOS.Menus;
using ProjectCHAOS.Common;
using ProjectCHAOS.Sandbox;

namespace ProjectCHAOS.GameLoops
{
	public class BasicGameLoopInfluencer : MonoBehaviour
	{
		[SerializeField]
		private StartMenuUI _startMenuUI = null;

		[SerializeField]
		private OnEnemyCollisionResponse _onPlayerEnemyCollisionResponse = null;

		[Space]
		[SerializeField]
		private Node _startMenu = null;

		[SerializeField]
		private Node _game = null;

		private void OnStartMenuTouchScreen()
		{
			_startMenu.Next();
		}

		private void OnPlayerDies()
		{
			_game.Next();
		}

		private void OnEnable()
		{
			_startMenuUI.OnTouchScreen += OnStartMenuTouchScreen;
			_onPlayerEnemyCollisionResponse.OnEnemyCollision += OnPlayerDies;
		}

		private void OnDisable()
		{
			_startMenuUI.OnTouchScreen -= OnStartMenuTouchScreen;
			_onPlayerEnemyCollisionResponse.OnEnemyCollision -= OnPlayerDies;
		}
	}
}
