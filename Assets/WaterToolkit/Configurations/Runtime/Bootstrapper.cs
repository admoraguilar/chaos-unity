using UnityEngine;
using UnityEngine.SceneManagement;

namespace WaterToolkit.Configurations
{
    [CreateAssetMenu(menuName = "WaterToolkit/Configurations/Bootstrapper")]
    public class Bootstrapper : ScriptableObjectSingleton<Bootstrapper>
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void RunOnLoad()
		{
			if(Instance == null) {
				Debug.LogWarning($"[{nameof(Bootstrapper)}] No {nameof(Bootstrapper)} detected.");
				return;
			}

			if(!Instance.isEnabled) { return; }

			foreach(GameObject prefab in Instance.prefabs) {
				GameObject prefabInstance = Instantiate(prefab);
				DontDestroyOnLoad(prefabInstance);
			}

			SceneManager.LoadSceneAsync(Instance.sceneName);
		}

		public bool isEnabled = false;
        public string sceneName = string.Empty;
		public GameObject[] prefabs = null;
	}
}
