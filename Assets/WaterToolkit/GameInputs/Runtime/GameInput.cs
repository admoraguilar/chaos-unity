using System;
using System.Collections.Generic;

namespace WaterToolkit.GameInputs
{
	public static class GameInput
	{
		private static Dictionary<Type, List<Controller>> _controllers = new Dictionary<Type, List<Controller>>();
		private static Dictionary<int, Player> _players = new Dictionary<int, Player>();

		public static Controller<T> GetController<T>(int index) where T : class, IController, new()
		{
			Type type = typeof(Controller<T>);
			if(!_controllers.TryGetValue(type, out List<Controller> controllers)) {
				_controllers[type] = controllers = new List<Controller>();
			}

			while(controllers.Count <= index) {
				controllers.Add(new Controller<T>());
			}

			return (Controller<T>)controllers[index];
		}

		public static Player GetPlayer(int index)
		{
			if(!_players.TryGetValue(index, out Player player)) {
				player = new Player();
				SetupPlayer(index, player);
			}
			return player;
		}

		public static void SetupPlayer(int index, Player player)
		{
			player.Initialize();
			_players[index] = player;
		}

		public static void ResetPlayer(int index)
		{
			if(_players.TryGetValue(index,out Player player)) {
				player.Deinitialize();
				_players.Remove(index);
			}
		}
	}
}