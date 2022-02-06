using ProjectCHAOS.Common;
using UnityEngine;

namespace ProjectCHAOS.Configurations
{
	[CreateAssetMenu(menuName = "ProjectCHAOS/Game Config")]
	public class GameConfig : ScriptableObjectSingleton<GameConfig>
	{
		public static int targetFramerate
		{
			get => Instance._targetFramerate;
			set {
				Instance._targetFramerate = value;
				Application.targetFrameRate = Instance._targetFramerate;
			}
		}

		[RuntimeInitializeOnLoadMethod]
		private static void RunOnLoad()
		{
			if(Instance == null) {
				Debug.LogWarning("There's no GameConfig, please create one.");
				return;
			}

			targetFramerate = targetFramerate;
		}



		[SerializeField]
		private int _targetFramerate = 60;
	}
}
