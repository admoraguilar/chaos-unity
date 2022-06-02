using UnityEngine;

namespace WaterToolkit.Blackboards
{
	public class BlackboardInitializer : ScriptableObjectSingleton<BlackboardInitializer>
	{
		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
		private static void RunOnLoad()
		{
			if(Instance._sceneInstancePrefab == null) { return; }

			GameObject sceneInstance = Instantiate(Instance._sceneInstancePrefab);
			sceneInstance.name = Instance._sceneInstancePrefab.name;
			DontDestroyOnLoad(sceneInstance);
		}

		[SerializeField]
		private GameObject _sceneInstancePrefab = null;
	}
}
