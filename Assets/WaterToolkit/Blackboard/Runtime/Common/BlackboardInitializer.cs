using UnityEngine;

namespace WaterToolkit.Blackboards
{
	[CreateAssetMenu(menuName = "WaterToolkit/Internal/Blackboard Initializer")]
	public class BlackboardInitializer : ScriptableObjectSingleton<BlackboardInitializer>
	{
		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
		private static void RunOnLoad()
		{
			if(Instance._sceneInstance == null) { return; }

			GameObject sceneInstance = Instantiate(Instance._sceneInstance);
			DontDestroyOnLoad(sceneInstance);
		}

		[SerializeField]
		private GameObject _sceneInstance = null;
	}
}
