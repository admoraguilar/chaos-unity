using System;
using UnityEngine;

namespace ProjectCHAOS.Systems
{
	public abstract class ValueBoardBuilder<
		TValueBoard, TValueObject, 
		TValueObjectBuider> : ScriptableObject
		where TValueBoard : ValueBoard<TValueObject>, new()
		where TValueObject : ValueObject
		where TValueObjectBuider : ValueBoardBuilder<TValueBoard, TValueObject, TValueObjectBuider>.ValueObjectBuilder<TValueObject>
	{
		[Serializable]
		public abstract class ValueObjectBuilder<TValueObjectBuilderObject> 
			where TValueObjectBuilderObject : TValueObject
		{
			public abstract TValueObjectBuilderObject Build();
		}

		[SerializeField]
		private TValueObjectBuider[] _objects = null;

		public TValueObjectBuider[] objects => _objects;

		public TValueBoard Build()
		{
			TValueBoard board = new TValueBoard();
			foreach(TValueObjectBuider obj in objects) {
				board.Add(obj.Build());
			}
			return board;
		}
	}
}

