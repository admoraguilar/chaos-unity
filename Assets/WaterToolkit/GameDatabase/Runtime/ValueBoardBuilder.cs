using System;
using UnityEngine;

namespace WaterToolkit.GameDatabases
{
	public abstract class GameDatabaseBuilder<
		TGameDatabase, TGameDatabaseObject, 
		TGameDatabaseObjectBuider> : ScriptableObject
		where TGameDatabase : GameDatabase<TGameDatabaseObject>, new()
		where TGameDatabaseObject : GameDatabaseObject
		where TGameDatabaseObjectBuider : GameDatabaseBuilder<TGameDatabase, TGameDatabaseObject, 
															  TGameDatabaseObjectBuider>
										  .ValueObjectBuilder<TGameDatabaseObject>
	{
		[Serializable]
		public abstract class ValueObjectBuilder<TValueObjectBuilderObject> 
			where TValueObjectBuilderObject : TGameDatabaseObject
		{
			public abstract TValueObjectBuilderObject Build();
		}

		[SerializeField]
		private TGameDatabaseObjectBuider[] _objects = null;

		public TGameDatabaseObjectBuider[] objects => _objects;

		public TGameDatabase Build()
		{
			TGameDatabase database = new TGameDatabase();
			foreach(TGameDatabaseObjectBuider obj in objects) {
				database.Add(obj.Build());
			}
			return database;
		}
	}
}

