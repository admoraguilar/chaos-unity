using UnityEngine;
using UnityEngine.SceneManagement;
using ProjectCHAOS.Utilities;

namespace ProjectCHAOS.Bootstrappers
{
    [CreateAssetMenu(menuName = "ProjectCHAOS/Bootstrapper")]
    public class Bootstrapper : ScriptableSingleton<Bootstrapper>
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void RunOnLoad()
		{
			if(!Instance.isEnabled) { return; }
			SceneManager.LoadSceneAsync(Instance.sceneName, LoadSceneMode.Additive);
		}



		public bool isEnabled = false;
        public string sceneName = string.Empty;
	}
}
