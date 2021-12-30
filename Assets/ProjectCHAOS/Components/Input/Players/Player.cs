using System.Collections.Generic;

namespace ProjectCHAOS.Inputs
{
	public class Player
	{
		private List<IInputMap> _maps = new List<IInputMap>();
		private MonoDelegate _monoDel = null;

		internal void Initialize()
		{
			_monoDel = MonoDelegate.GetOrCreate($"{nameof(GameInput)}.{nameof(Player)}", true);
		}

		internal void Deinitialize()
		{

		}

		public T GetMap<T>() where T : class => (T)_maps.Find(m => (m as T) != null);

		public void AddMap(IInputMap map)
		{
			_maps.Add(map);
			_monoDel.UpdateCallback += map.Update;
			_monoDel.FixedUpdateCallback += map.FixedUpdate;
			_monoDel.LateUpdateCallback += map.LateUpdate;
		}

		public void RemoveMap(IInputMap map)
		{
			_maps.Remove(map);
			_monoDel.UpdateCallback -= map.Update;
			_monoDel.FixedUpdateCallback -= map.FixedUpdate;
			_monoDel.LateUpdateCallback -= map.LateUpdate;
		}
	}
}
