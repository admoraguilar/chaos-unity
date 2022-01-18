using System;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;

namespace ProjectCHAOS.Utilities
{
	public static class TypeUtilities
	{
		public static string GetClassName(this Type type)
		{
			string result = type.Name;
			if(type.IsGenericType) {
				string name = type.Name.Substring(0, type.Name.IndexOf('`'));
				Type[] genericTypes = type.GenericTypeArguments;
				result = $"{name}<{string.Join(",", genericTypes.Select(GetClassName))}>";
			}
			return result;
		}

		public static Type[] GetAllTypeDerivatives(this Type type, bool isIncludeSelf = false)
		{
			Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
			List<Type> derivatives = new List<Type>();

			foreach(Assembly assembly in assemblies) {
				derivatives.AddRange(assembly.GetTypes().Where(t => (t != type || isIncludeSelf) &&
																	 t.IsSubclassOf(type)));
			}

			return derivatives.ToArray();
		}

		public static Type[] GetAllTypeBases(this Type type, bool isIncludeSelf = false)
		{
			Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
			List<Type> bases = new List<Type>();

			foreach(Assembly assembly in assemblies) {
				bases.AddRange(assembly.GetTypes().Where(t => (t != type || isIncludeSelf) &&
																t != typeof(object) &&
																t.IsAssignableFrom(type)));
			}

			return bases.ToArray();
		}

		public static Type[] GetAllTypeBases(this Type type, Type until, bool isIncludeSelf = false, bool isIncludeUntil = false)
		{
			List<Type> bases = new List<Type>();

			if(until.IsAssignableFrom(type)) {
				if(isIncludeSelf) {
					bases.Add(type);
				}

				while(type.BaseType != until) {
					type = type.BaseType;
					bases.Add(type);
				}

				if(isIncludeUntil) {
					bases.Add(until);
				}
			}

			return bases.ToArray();
		}
	}

}
