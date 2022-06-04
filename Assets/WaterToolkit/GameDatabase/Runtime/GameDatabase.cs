using System;
using System.Collections.Generic;
using UnityEngine;

namespace WaterToolkit.GameDatabases
{
	[CreateAssetMenu(menuName = "WaterToolkit/Game Databases/Database")]
	public class GameDatabase : ScriptableObject
	{
		[SerializeField]
		private GameCollection[] _collections = null;

		public int Count
		{
			get {
				int result = 0;
				foreach(GameCollection col in _collections) {
					result += col.Count;
				}
				return result;
			}
		}

		public bool TryGet<T>(Predicate<T> match)
		{
			bool result = false;
			CollectionOperation<T>(col => {
				if(!result) { result = col.TryGet(match); }
			});
			return result;
		}

		public List<T> Get<T>(
			Predicate<T> match, int count = -1,
			IComparer<T> sort = null)
		{
			List<T> results = new List<T>();
			CollectionOperation<T>(col => {
				List<T> colResults = col.Get(match, count);
				results.AddRange(colResults);
			});
			if(sort != null) { results.Sort(sort); }
			return results;
		}

		private void CollectionOperation<T>(Action<GameCollection<T>> onOperate)
		{
			foreach(GameCollection collection in _collections) {
				GameCollection<T> col = collection as GameCollection<T>;
				if(col != null) { onOperate(col); }
			}
		}
	}
}
