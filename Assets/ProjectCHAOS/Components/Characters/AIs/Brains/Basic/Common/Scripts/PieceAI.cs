using UnityEngine;
using WaterToolkit.Behave;

namespace ProjectCHAOS.Characters.AIs
{
	public class PieceAI : MonoBehaviour, IHealth
	{
		[SerializeField]
		private Health _health = null;

		public Health health => _health;
	}
}
