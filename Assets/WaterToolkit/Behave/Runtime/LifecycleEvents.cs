using System;
using UnityEngine;

namespace WaterToolkit.Behave
{
	public class LifecycleEvents : MonoBehaviour
	{
		public event Action OnAwakeResponse = delegate { };
		public event Action OnStartResponse = delegate { };
		public event Action OnEnableResponse = delegate { };
		public event Action OnDisableResponse = delegate { };
		public event Action OnDestroyResponse = delegate { };

		private void Awake() => OnAwakeResponse();
		private void Start() => OnStartResponse();
		private void OnEnable() => OnEnableResponse();
		private void OnDisable() => OnDisableResponse();
		private void OnDestroy() => OnDestroyResponse();
	}
}
