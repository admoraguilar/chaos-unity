using System;
using UnityEngine;

using UObject = UnityEngine.Object;

namespace ProjectCHAOS
{
	[CreateAssetMenu(menuName = "ProjectCHAOS/Game Input")]
	public class GameInput : ScriptableSingleton<GameInput>
	{
		private class GameInputMono : MonoBehaviour
		{
			public event Action UpdateCallback = delegate { };
			public event Action FixedUpdateCallback = delegate { };
			public event Action LateUpdateCallback = delegate { };

			private void Update() => UpdateCallback();
			private void FixedUpdate() => FixedUpdateCallback();
			private void LateUpdate() => LateUpdateCallback();
		}



		public static IMovementInputMap movementInputMap => Instance._movementInputMap;
		public static ICombatInputMap combatInputMap => Instance._combatInputMap;

		[RuntimeInitializeOnLoadMethod]
		private static void RunOnLoad()
		{
			Instance.Initialize();
		}


		private GameInputMono _mono = null;

		private IMovementInputMap _movementInputMap = new MobileMovementInputMap();
		private ICombatInputMap _combatInputMap = new PCCombatInputMap();

		private void Initialize()
		{
			GameObject monoGO = new GameObject(nameof(GameInput));
			UObject.DontDestroyOnLoad(monoGO);

			_mono = monoGO.AddComponent<GameInputMono>();
			RegisterInputMap(_mono, _movementInputMap);
			RegisterInputMap(_mono, _combatInputMap);

			void RegisterInputMap(GameInputMono mono, IInputMap inputMap)
			{
				mono.UpdateCallback += inputMap.Update;
				mono.FixedUpdateCallback += inputMap.FixedUpdate;
				mono.LateUpdateCallback += inputMap.LateUpdate;
			}
		}
	}
}
