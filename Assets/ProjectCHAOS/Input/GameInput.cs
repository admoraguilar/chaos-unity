using System;
using UnityEngine;

namespace ProjectCHAOS
{
    public class GameInput
    {
        public static IMovementInputMap movementInputMap => _instance._movementInputMap;
        public static ICombatInputMap combatInputMap => _instance._combatInputMap;

        private static GameInput _instance = null;

        private IMovementInputMap _movementInputMap = new PCMovementInputMap();
        private ICombatInputMap _combatInputMap = new PCCombatInputMap();

        public void Init()
		{
            GameInputMono mono = new GameObject(nameof(GameInput)).AddComponent<GameInputMono>();
            UnityEngine.Object.DontDestroyOnLoad(mono.gameObject);
        }

        [RuntimeInitializeOnLoadMethod]
        private static void RunOnLoad()
		{
            if(_instance != null) { return; }
            _instance = new GameInput();
            _instance.Init();
		}

        private class GameInputMono : MonoBehaviour
		{
            public event Action UpdateCallback = delegate { };

			private void Update() => UpdateCallback();
        }
    }
}
