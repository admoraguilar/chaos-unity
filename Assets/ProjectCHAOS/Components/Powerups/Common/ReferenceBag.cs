using System;
using System.Collections.Generic;
using System.Linq;
using WaterToolkit;

namespace ProjectCHAOS.Powerups
{
	public class ReferenceBag
	{
		private Dictionary<Type, List<object>> _referencesDict = new Dictionary<Type, List<object>>();

		public IEnumerable<T> GetMany<T>()
		{
			IEnumerable<T> result = default;

			Type type = typeof(T);
			if(_referencesDict.ContainsKey(type)) {
				List<object> refList = _referencesDict[type];
				result = refList.Cast<T>();
			}

			return result;
		}

		public T Get<T>()
		{
			T result = default;

			Type type = typeof(T);
			if(_referencesDict.ContainsKey(type)) {
				List<object> refList = _referencesDict[type];
				result = (T)refList[0];
			}

			return result;
		}

		public void AddRange(IEnumerable<object> references)
		{
			foreach(object reference in references) {
				Add(reference);
			}
		}

		public void Add(object reference)
		{
			ReferenceCrawlOperation(reference, (object reference, Type type, List<object> refList) => {
				if(!refList.Contains(reference)) {
					refList.Add(reference);
				}
			});
		}

		public void RemoveRange(IEnumerable<object> references)
		{
			foreach(object reference in references) {
				Remove(reference);
			}
		}

		public void Remove(object reference)
		{
			ReferenceCrawlOperation(reference, (object reference, Type type, List<object> refList) => {
				if(refList.Contains(reference)) {
					refList.Remove(reference);
				}

				if(refList.Count <= 0) {
					_referencesDict.Remove(type);
				}
			});
		}

		private void ReferenceCrawlOperation(object reference, Action<object, Type, List<object>> onOperate)
		{
			Type[] allTypes = reference.GetType().GetAllTypeDerivatives(true);
			foreach(Type type in allTypes) {
				if(!_referencesDict.TryGetValue(type, out List<object> refList)) {
					_referencesDict[type] = refList = new List<object>();
				} else {
					refList.RemoveAll(r => r == null);
				}

				onOperate(reference, type, refList);
			}
		}

		public ReferenceBag Clone()
		{
			return new ReferenceBag {
				_referencesDict = _referencesDict.ToDictionary(d => d.Key, d => d.Value)
			};
		}
	}
}
