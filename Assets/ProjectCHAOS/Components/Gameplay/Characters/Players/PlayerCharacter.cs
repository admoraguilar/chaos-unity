using UnityEngine;
using ProjectCHAOS.Gameplay.Behave;

namespace ProjectCHAOS.Gameplay.Characters.Players
{
	public class PlayerCharacter : MonoBehaviour
	{
		[SerializeField]
		private Health _health = new Health();

		public Health health => _health;
	}
}
