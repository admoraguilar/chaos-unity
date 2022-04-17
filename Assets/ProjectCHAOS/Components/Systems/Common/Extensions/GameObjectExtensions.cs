using UnityEngine;

namespace ProjectCHAOS.Systems
{
	public static class GameObjectExtensions
	{
		public static T GetCachedComponent<T>(this GameObject gameObject, ref T cache) where T : Component
		{
			if(cache == null) { cache = gameObject.GetComponent<T>(); }
			return cache;
		}

		public static T GetComponentInParentAndChildren<T>(this GameObject gameObject, bool includeInactive = false) where T : Component
		{
			T result = gameObject.GetComponentInParent<T>(includeInactive);
			if(result == null) { result = gameObject.GetComponentInChildren<T>(includeInactive); }
			return result;
		}
	}
}
