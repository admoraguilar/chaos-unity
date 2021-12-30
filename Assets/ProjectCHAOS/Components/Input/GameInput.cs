using System.Collections.Generic;
using UnityEngine;

namespace ProjectCHAOS.Inputs
{
	[CreateAssetMenu(menuName = "ProjectCHAOS/Game Input")]
	public class GameInput : ScriptableSingleton<GameInput>
	{
		internal static void Initialize()
		{
			foreach(IInputControl control in Instance._controls) { control.Initialize(); }
			foreach(Player player in Instance._players.Values) { player.Initialize(); }
		}

		public static Player GetPlayer(int index)
		{
			if(!Instance._players.TryGetValue(index, out Player player)) {
				Instance._players[index] = player = CreateDefaultPlayer();
			}
			return player;

			Player CreateDefaultPlayer()
			{
				Player player = new Player();
				player.Initialize();
				player.AddMap(new MobileMovementInputMap());
				player.AddMap(new PCCombatInputMap());
				return player;
			}
		}

		public static void AddPlayer(Player player)
		{

		}

		public static void RemovePlayer(Player player)
		{

		}

		[RuntimeInitializeOnLoadMethod]
		private static void RunOnLoad()
		{
			Initialize();
		}



		private List<IInputControl> _controls = new List<IInputControl>();
		private Dictionary<int, Player> _players = new Dictionary<int, Player>();
	}
}
