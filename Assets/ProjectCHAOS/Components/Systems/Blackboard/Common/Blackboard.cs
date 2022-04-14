using System;
using System.Collections.Generic;

namespace ProjectCHAOS.Systems.Blackboards
{
	public static class Blackboard
	{
		private static Dictionary<Type, List<IBlackboard>> _blackboards = new Dictionary<Type, List<IBlackboard>>();

		public static void Add<T>(T blackboard) where T : IBlackboard =>
			GetBlackboardContainer(typeof(T)).Add(blackboard);

		public static void Remove<T>(T blackboard) where T : IBlackboard =>
			GetBlackboardContainer(typeof(T)).Remove(blackboard);

		public static T Get<T>()
		{
			List<IBlackboard> blackboards = GetBlackboardContainer(typeof(T));
			if(blackboards.Count < 0) {
				throw new NullReferenceException("There's no blackboard of this type. Please ensure that there's an existing one.");
			}
			return (T)blackboards[0];
		}

		private static List<IBlackboard> GetBlackboardContainer(Type type)
		{
			if(!_blackboards.TryGetValue(type, out List<IBlackboard> blackboards)) {
				_blackboards[type] = blackboards = new List<IBlackboard>();
			}
			return blackboards;
		}
	}
}

