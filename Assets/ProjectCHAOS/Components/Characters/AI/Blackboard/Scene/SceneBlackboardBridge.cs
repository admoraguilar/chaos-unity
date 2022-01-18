using UnityEngine;

namespace ProjectCHAOS
{
	public class SceneBlackboardBridge : MonoBehaviour
	{
		private SceneBlackboard _sceneBlackboard = null;

		private void Awake() =>
			_sceneBlackboard = Blackboard.Get<SceneBlackboard>();

		private void OnEnable() =>
			_sceneBlackboard.Add(this);

		private void OnDisable() =>
			_sceneBlackboard.Remove(this);
	}
}
