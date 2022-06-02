using UnityEngine;
using WaterToolkit.Scores;
using ProjectCHAOS.Characters.Players;

namespace ProjectCHAOS.GameSerialization
{
	public class GameSerializer : MonoBehaviour
	{
		private ScorerSerializer _scorerSerializer = null;
		private PlayerStatsSerializer _playerStatsSerializer = null;

		public void Initialize(Scorer scorer, PlayerCharacter playerCharacter)
		{
			_scorerSerializer = new ScorerSerializer("scores", scorer);
			_playerStatsSerializer = new PlayerStatsSerializer("playerStats", playerCharacter);
		}

		public void Load()
		{
			_scorerSerializer.Load();
			_playerStatsSerializer.Load();
		}

		public void Save()
		{
			_scorerSerializer.Save();
			_playerStatsSerializer.Save();
		}

		public void Clear()
		{
			_scorerSerializer.Clear();
			_playerStatsSerializer.Clear();
		}
	}
}
