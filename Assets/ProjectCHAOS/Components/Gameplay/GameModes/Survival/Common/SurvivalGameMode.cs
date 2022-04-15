
using UnityEngine;
namespace ProjectCHAOS.Gameplay.GameModes.Survival
{
	[DefaultExecutionOrder(-1)]
	public class SurvivalGameMode : MonoBehaviour
	{
		[SerializeField]
		private SurvivalSystem _system = null;

		[SerializeField]
		private SurvivalWorld _world = null;

		[SerializeField]
		private SurvivalFlow _flow = null;

		private void Awake()
		{
			_system.Awake(_world);
			_flow.Awake(_world, _system);
		}

		private void OnEnable()
		{
			_system.OnEnable();
			_world.OnEnable();
			_flow.OnEnable();
		}

		private void OnDisable()
		{
			_system.OnDisable();
			_world.OnDisable();
			_flow.OnDisable();
		}
	}
}
