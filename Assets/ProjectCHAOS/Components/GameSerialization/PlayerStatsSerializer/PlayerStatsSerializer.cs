using ProjectCHAOS.Characters;
using ProjectCHAOS.Characters.Players;

namespace ProjectCHAOS.GameSerialization
{
	public class PlayerStatsSerializer : GameDataSerializer
	{
		private PlayerCharacter _character = null;

		public PlayerStatsSerializer(string fileName, PlayerCharacter playerCharacter) :
			base(string.Empty, fileName)
		{
			_character = playerCharacter;
		}

		public override void Load()
		{
			PlayerStatsDataV1 data = dataSerializer.LoadObjectVersion<PlayerStatsDataV1>();
			CharacterStats stats = _character.characterStats;
			stats.speed.value = data.speed;
			stats.fireRate.value = data.fireRate;
		}

		public override void Save()
		{
			PlayerStatsDataV1 data = new PlayerStatsDataV1();
			CharacterStats stats = _character.characterStats;
			data.speed = stats.speed.value;
			data.fireRate = stats.fireRate.value;
			dataSerializer.SaveObjectVersion(data);
		}
	}
}
