using UnityEngine;
using UnityEngine.SceneManagement;
using ProjectCHAOS.Systems;

namespace ProjectCHAOS.Systems.Configurations
{
    [CreateAssetMenu(menuName = "ProjectCHAOS/Configurations/Bootstrapper")]
    public class Bootstrapper : ScriptableObjectSingleton<Bootstrapper>
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void RunOnLoad()
		{
			if(Instance == null) {
				Debug.LogWarning($"[{typeof(Bootstrapper)}] There's no GameConfig, please create one.");
				return;
			}

			if(!Instance.isEnabled) { return; }
			SceneManager.LoadSceneAsync(Instance.sceneName, LoadSceneMode.Additive);
		}

		public bool isEnabled = false;
        public string sceneName = string.Empty;
	}
}
