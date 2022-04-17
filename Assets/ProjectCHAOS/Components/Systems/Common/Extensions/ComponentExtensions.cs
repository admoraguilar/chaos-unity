using UnityEngine;

namespace ProjectCHAOS.Systems
{
    public static class ComponentExtensions
    {
        public static T GetCachedComponent<T>(this Component component, ref T cache) where T : Component
		{
            if(cache == null) { cache = component.GetComponent<T>(); }
            return cache;
		}

		public static T GetComponentInParentAndChildren<T>(this Component component, bool includeInactive = false) where T : Component
		{
			T result = component.GetComponentInParent<T>(includeInactive);
			if(result == null) { result = component.GetComponentInChildren<T>(includeInactive); }
			return result;
		}
    }
}
