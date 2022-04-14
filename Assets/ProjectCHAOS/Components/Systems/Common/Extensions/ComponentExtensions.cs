using UnityEngine;

namespace ProjectCHAOS.Common
{
    public static class ComponentExtensions
    {
        public static T GetCachedComponent<T>(this Component component, ref T cache) where T : Component
		{
            if(cache == null) { cache = component.GetComponent<T>(); }
            return cache;
		}
    }
}
