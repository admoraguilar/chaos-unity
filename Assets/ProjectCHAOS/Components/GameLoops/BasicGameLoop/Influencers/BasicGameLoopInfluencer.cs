using UnityEngine;
using UnityEngine.EventSystems;
using ProjectCHAOS.Common;
using ProjectCHAOS.Scores;
using ProjectCHAOS.Sandbox;
using ProjectCHAOS.GUI.Menus;

namespace ProjectCHAOS.GameLoops
{
	public class BasicGameLoopInfluencer : MonoBehaviour
	{
		[SerializeField]
		private StartMenuUI _startMenuUI = null;

		[SerializeField]
		private OnEnemyCollisionResponse _onPlayerEnemyCollisionResponse = null;

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

		private void OnPlayerDies()
		{
			//_game.Next();
			//_joystick.OnPointerUp(new PointerEventData(EventSystem.current));
		}

		private void Awake()
		{
			_score = Scorer.Instance.GetScore(0);
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
