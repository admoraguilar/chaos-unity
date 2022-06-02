using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

using UObject = UnityEngine.Object;

#if UNITY_EDITOR

using UnityEditor;

#endif

namespace WaterToolkit
{
	public class ScriptableObjectSingleton<T> : ScriptableObject where T : ScriptableObject
	{
		private static T _instance = default;

		public static T Instance
		{
			get {
				if(_instance == null) {
					throw new NullReferenceException($"No scriptable object instance found. Make sure you have one initialized for [{nameof(T)}] on Player Settings -> Other Settings -> Preloaded Assets.");
				}
				return _instance;
			}
		}

		private void OnEnable()
		{
#if UNITY_EDITOR

			List<UObject> preloadedAssets = PlayerSettings.GetPreloadedAssets().ToList();
			preloadedAssets.RemoveAll(asset => object.ReferenceEquals(asset, null));

#endif

			if(_instance != null && _instance != this) {
				DestroyImmediate(this, true);
				return;
			}

			_instance = this as T;

#if UNITY_EDITOR

			if(!preloadedAssets.Contains(_instance)) {
				preloadedAssets.Add(_instance);
				PlayerSettings.SetPreloadedAssets(preloadedAssets.ToArray());
			}
			
#endif
		}

		private void OnDisable()
		{
			if(_instance == this) {
				_instance = null;
			}
		}
	}


#if UNITY_EDITOR

	class _ScriptableSingletonEditor
	{
		[InitializeOnLoadMethod]
		private static void OnEditorInitialize()
		{
			// Touch the preloaded assets so it'll always be loaded upon the opening of the editor
			PlayerSettings.GetPreloadedAssets();

			EditorApplication.projectChanged += OnProjectChanged;
		}

		private static void OnProjectChanged()
		{
			List<UObject> preloadedAssets = PlayerSettings.GetPreloadedAssets().ToList();
			preloadedAssets.RemoveAll(asset => object.ReferenceEquals(asset, null));
			PlayerSettings.SetPreloadedAssets(preloadedAssets.ToArray());
		}
	}

#endif
}
