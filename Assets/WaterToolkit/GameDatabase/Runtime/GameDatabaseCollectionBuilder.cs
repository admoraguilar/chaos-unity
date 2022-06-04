using System;
using UnityEngine;

namespace WaterToolkit.GameDatabases
{
	public abstract class GameDatabaseCollectionBuilder<
		TGameDatabaseCollection, TGameDatabaseCollectionObject, 
		TGameDatabaseCollectionObjectBuider> : ScriptableObject
		where TGameDatabaseCollection : GameDatabaseCollection<TGameDatabaseCollectionObject>, new()
		where TGameDatabaseCollectionObject : GameDatabaseCollectionObject
		where TGameDatabaseCollectionObjectBuider : GameDatabaseCollectionBuilder<TGameDatabaseCollection, TGameDatabaseCollectionObject, 
															  TGameDatabaseCollectionObjectBuider>
										  .ValueObjectBuilder<TGameDatabaseCollectionObject>
	{
		[Serializable]
		public abstract class ValueObjectBuilder<TValueObjectBuilderObject> 
			where TValueObjectBuilderObject : TGameDatabaseCollectionObject
		{
			public abstract TValueObjectBuilderObject Build();
		}

		[SerializeField]
		private TGameDatabaseCollectionObjectBuider[] _objects = null;

		public TGameDatabaseCollectionObjectBuider[] objects => _objects;

		public TGameDatabaseCollection Build()
		{
			TGameDatabaseCollection collection = new TGameDatabaseCollection();
			foreach(TGameDatabaseCollectionObjectBuider obj in objects) {
				collection.Add(obj.Build());
			}
			return collection;
		}
	}
}

