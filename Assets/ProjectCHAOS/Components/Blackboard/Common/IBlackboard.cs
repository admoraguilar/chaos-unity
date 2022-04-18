using System.Collections.Generic;

namespace ProjectCHAOS.Blackboards
{
	public interface IBlackboard
	{
		public void Add<T>(T instance) where T : class;
		public void AddMany<T>(IEnumerable<T> instances) where T : class;
		public void Remove<T>(T instance) where T : class;
		public void RemoveMany<T>(IEnumerable<T> instances) where T : class;
		public T Get<T>() where T : class;
		public IEnumerable<T> GetMany<T>() where T : class;
	}
}
