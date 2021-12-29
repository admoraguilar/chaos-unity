using UnityEngine;
using UnityEngine.SceneManagement;

namespace ProjectCHAOS.Bootup
{
    [CreateAssetMenu(menuName = "ProjectCHAOS/Bootstrapper")]
    public class Bootstrapper : ScriptableObject
    {
		private static Bootstrapper _instance = null;

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void RunOnLoad()
		{
			if(!_instance.isEnabled) { return; }
			SceneManager.LoadSceneAsync(_instance.sceneName, LoadSceneMode.Additive);
		}

		public bool isEnabled = false;
        public string sceneName = string.Empty;

		private void OnEnable()
		{
			if(_instance != null) {
				Destroy(this);
				return;
			}

			_instance = this;
		}

		private void OnDisable()
		{
			if(_instance == this) {
				_instance = null;
			}
		}
	}
}
