using System;
using System.Collections.Generic;

namespace WaterToolkit
{
	[Serializable]
	public class FlyweightSpec<T> : Flyweight<T> where T : FlyweightSpecObject<T>
	{
		private List<object> _references = new List<object>();

		public void AddReferences(IEnumerable<object> references)
		{
			_references.AddRange(references);
		}

		protected override void Internal_OnAdd(T obj)
		{
			obj.Initialize(_references);
		}
	}
}
