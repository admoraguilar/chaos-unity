using UnityEngine;

namespace WaterToolkit.Configurations
{
	[CreateAssetMenu(menuName = "WaterToolkit/Configurations/Game Config")]
	public class GameConfig : ScriptableObjectSingleton<GameConfig>
	{
		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
		private static void RunOnLoad()
		{
			if(Instance == null) {
				Debug.LogWarning($"[{nameof(GameConfig)}] No {nameof(GameConfig)} detected in (Player Settings -> Other Settings -> Preloaded Assets).");
				return;
			}

			Instance.targetFramerate = Instance.targetFramerate;
		}

		[SerializeField]
		private int _targetFramerate = 60;

		public int targetFramerate
		{
			get => _targetFramerate;
			set {
				_targetFramerate = value;
				Application.targetFrameRate = _targetFramerate;
			}
		}
	}
}
