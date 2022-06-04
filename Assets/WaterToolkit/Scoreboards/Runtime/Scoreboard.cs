using UnityEngine;
using WaterToolkit.GameDatabases;

namespace WaterToolkit.Scoreboards
{
	[CreateAssetMenu(menuName = "WaterToolkit/Scoreboards/Scoreboard")]
	public class Scoreboard : GameCollection<ScoreObject> { }
}