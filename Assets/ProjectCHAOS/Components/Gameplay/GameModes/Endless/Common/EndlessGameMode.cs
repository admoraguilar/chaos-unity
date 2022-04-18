using UnityEngine;

namespace ProjectCHAOS.Gameplay.GameModes.Endless
{
	/// <summary>
	/// Could be split to small sub-classes in order to have better readability.
	/// </summary>
	[DefaultExecutionOrder(-1)]
	public class EndlessGameMode : GameMode<EndlessWorld, EndlessSystem, EndlessFlow> { }
}
