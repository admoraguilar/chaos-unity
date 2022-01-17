using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

using SObject = System.Object;
using UObject = UnityEngine.Object;

namespace ProjectCHAOS
{
	public class SceneBlackboard : MonoBehaviour, IBlackboard
	{
		private Dictionary<Type, List<SObject>> _blackboard = new Dictionary<Type, List<SObject>>();

		public void Add<T>(T instance) where T : class
		{
			DoBlackboardOperation(
				instance.GetType(), instance, 
				(Type type, T inst) => {
					List<SObject> container = GetBlackboardContainer(type);
					container.Add(inst);
				}
			);
		}

		public void AddMany<T>(IEnumerable<T> instances) where T : class
		{
			foreach(T instance in instances) {
				Add(instance);
			}
		}

		public void Remove<T>(T instance) where T : class
		{
			DoBlackboardOperation(
				instance.GetType(), instance,
				(Type type, T inst) => {
					List<SObject> container = GetBlackboardContainer(type);
					container.Remove(inst);
				}
			);
		}

		public void RemoveMany<T>(IEnumerable<T> instances) where T : class
		{
			foreach(T instance in instances) {
				Remove(instance);	
			}
		}

		public T Get<T>() where T : class
		{
			List<SObject> container = GetBlackboardContainer(typeof(T));
			return (T)container[0];
		}

		public IEnumerable<T> GetMany<T>() where T : class
		{
			List<SObject> container = GetBlackboardContainer(typeof(T));
			return container.Cast<T>();
		}

		private Dictionary<Type, UObject> GetMonoPairs(MonoBehaviour mono)
		{
			Dictionary<Type, UObject> result = new Dictionary<Type,UObject>();

			Component[] components = mono.GetComponents<Component>();
			foreach(Component component in components) {
				Type componentType = component.GetType();
				Type[] allComponentTypes = TypeUtilities.GetAllTypeBases(componentType, true);
				foreach(Type type in allComponentTypes) {
					result[type] = component;
				}
			}

			return result;
		}

		private List<SObject> GetBlackboardContainer(Type type)
		{
			if(!_blackboard.TryGetValue(type, out List<SObject> container)) {
				_blackboard[type] = container = new List<SObject>();
			}
			return container;
		}

		private void DoBlackboardOperation<T>(
			Type type, T instance, 
			Action<Type, T> operation) where T : class
		{
			MonoBehaviour mono = instance as MonoBehaviour;
			bool isMono = mono != null;

			if(isMono) {
				Dictionary<Type, UObject> monoPairs = GetMonoPairs(mono);
				foreach(KeyValuePair<Type, UObject> monoPair in monoPairs) {
					operation(monoPair.Key, monoPair.Value as T);
				}
			} else {
				operation(instance.GetType(), instance);
			}
		}
	}
}
