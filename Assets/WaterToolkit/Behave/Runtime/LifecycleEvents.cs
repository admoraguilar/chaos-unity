using System;
using UnityEngine;

#if UNITY_EDITOR

using UnityEditor;

#endif

namespace WaterToolkit.Behave
{
	public class LifecycleEvents : MonoBehaviour
	{
		public event Action OnAwakeResponse = delegate { };
		public event Action OnStartResponse = delegate { };
		public event Action OnEnableResponse = delegate { };
		public event Action OnDisableResponse = delegate { };
		public event Action OnDestroyResponse = delegate { };

#if UNITY_EDITOR

		private bool _isEditModeOrSwitchingToEditMode = false;

#endif

		private void Awake()
		{
			OnAwakeResponse();

#if UNITY_EDITOR

			EditorApplication.playModeStateChanged += OnPlayModeStateChange;

#endif
		}

		private void Start()
		{
			OnStartResponse();
		}

		private void OnEnable()
		{
			OnEnableResponse();
		}

		private void OnDisable()
		{
			OnDisableResponse();
		}

		private void OnDestroy()
		{
			bool shouldCallResponse = true;

#if UNITY_EDITOR

			if(_isEditModeOrSwitchingToEditMode) {
				shouldCallResponse = false;
			}

#endif

			if(shouldCallResponse) { OnDestroyResponse(); }

#if UNITY_EDITOR

			EditorApplication.playModeStateChanged -= OnPlayModeStateChange;

#endif
		}

#if UNITY_EDITOR

		private void OnPlayModeStateChange(PlayModeStateChange mode)
		{
			if(mode == PlayModeStateChange.ExitingPlayMode) {
				_isEditModeOrSwitchingToEditMode = true;
			} else if(mode == PlayModeStateChange.EnteredEditMode) {
				_isEditModeOrSwitchingToEditMode = true;
			} else {
				_isEditModeOrSwitchingToEditMode = false;
			}
		}

#endif
	}
}
