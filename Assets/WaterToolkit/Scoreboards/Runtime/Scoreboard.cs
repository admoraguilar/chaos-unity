using System;
using WaterToolkit.GameDatabases;

namespace WaterToolkit.Scoreboards
{
	[Serializable]
    public class Scoreboard : GameDatabaseCollection<ScoreObject> { }
}