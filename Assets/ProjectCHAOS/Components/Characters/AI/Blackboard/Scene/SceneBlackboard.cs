using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using ProjectCHAOS.Utilities;

using SObject = System.Object;
using UObject = UnityEngine.Object;

namespace ProjectCHAOS.Blackboards
{
	[DefaultExecutionOrder(-1)]
	public class SceneBlackboard : MonoBehaviour, IBlackboard
	{
		private Dictionary<Type, List<SObject>> _blackboard = new Dictionary<Type, List<SObject>>();

		public void Add<T>(T instance) where T : class
		{
			DoBlackboardOperation(instance, (Type type, SObject inst) => {
				List<SObject> container = GetBlackboardContainer(type);
				container.Add(inst);
			});
		}

		public void AddMany<T>(IEnumerable<T> instances) where T : class
		{
			foreach(T instance in instances) {
				Add(instance);
			}
		}

		public void Remove<T>(T instance) where T : class
		{
			DoBlackboardOperation(instance, (Type type, SObject inst) => {
				List<SObject> container = GetBlackboardContainer(type);
				container.Remove(inst);
			});
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

		private List<SObject> GetBlackboardContainer(Type type)
		{
			if(!_blackboard.TryGetValue(type, out List<SObject> container)) {
				_blackboard[type] = container = new List<SObject>();
			}
			return container;
		}

		private void DoBlackboardOperation(SObject instance, Action<Type, SObject> operation)
		{
			MonoBehaviour mono = instance as MonoBehaviour;
			bool isMono = mono != null;

			if(isMono) {
				Dictionary<Type, UObject> monoPairs = GetMonoPairs(mono);
				foreach(KeyValuePair<Type, UObject> monoPair in monoPairs) {
					operation(monoPair.Key, monoPair.Value);
				}
			} else {
				operation(instance.GetType(), instance);
			}
		}

		private Dictionary<Type, UObject> GetMonoPairs(MonoBehaviour mono)
		{
			Dictionary<Type, UObject> result = new Dictionary<Type, UObject>();

			Component[] components = mono.GetComponents<Component>();
			foreach(Component component in components) {
				Type componentType = component.GetType();
				Type[] allComponentTypes = TypeUtilities.GetAllTypeBases(componentType, typeof(MonoBehaviour), true, false);
				foreach(Type type in allComponentTypes) {
					result[type] = component;
				}
			}

			return result;
		}

		private void OnEnable()
		{
			Blackboard.Add(this);
		}

		private void OnDisable()
		{
			Blackboard.Remove(this);
		}
	}
}
