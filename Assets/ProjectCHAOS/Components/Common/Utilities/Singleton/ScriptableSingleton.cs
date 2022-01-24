using System;
using UnityEngine;

namespace ProjectCHAOS.Common
{
	public class ScriptableSingleton<T> : ScriptableObject where T : ScriptableObject
	{
		public static T Instance
		{
			get {
				if(_instance == null) {
					throw new NullReferenceException($"No scriptable object instance found. Make sure you have one initialized for [{nameof(T)}] on Player Settings -> Other Settings -> Preloaded Assets.");
				}
				return _instance;
			}
		}
		private static T _instance = default;

		private void OnEnable()
		{
			if(_instance != null && _instance != this) {
				DestroyImmediate(this, true);
				return;
			}

			_instance = this as T;
		}

		private void OnDisable()
		{
			if(_instance == this) {
				_instance = null;
			}
		}
	}
}
