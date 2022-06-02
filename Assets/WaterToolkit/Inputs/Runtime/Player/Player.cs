using System.Collections.Generic;
using WaterToolkit;

namespace WaterToolkit.Inputs
{
	public class Player
	{
		private static MonoDelegate monoDel
		{
			get {
				if(_monoDel == null) {
					_monoDel = MonoDelegate.GetOrCreate(
						$"{nameof(MInput)}.{nameof(Player)}", true);
				}
				return _monoDel;
			}
		}
		private static MonoDelegate _monoDel = null;



		private List<IMap> _maps = new List<IMap>();

		internal void Initialize() { }

		internal void Deinitialize()
		{
			foreach(IMap map in _maps) {
				RemoveMap(map);
			}
		}

		public T GetMap<T>() where T : class => (T)_maps.Find(m => (m as T) != null);

		public void AddMap(IMap map)
		{
			map.Initialize();

			monoDel.UpdateCallback += map.Update;
			monoDel.FixedUpdateCallback += map.FixedUpdate;
			monoDel.LateUpdateCallback += map.LateUpdate;

			_maps.Add(map);
		}

		public void RemoveMap(IMap map)
		{
			map.Deinitialize();

			monoDel.UpdateCallback -= map.Update;
			monoDel.FixedUpdateCallback -= map.FixedUpdate;
			monoDel.LateUpdateCallback -= map.LateUpdate;

			_maps.Remove(map);
		}
	}
}
