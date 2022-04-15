using UnityEngine;
using ProjectCHAOS.Systems;
using ProjectCHAOS.Gameplay.Behave;

namespace ProjectCHAOS.Gameplay.Characters.Players
{
	public class PlayerCharacter : MonoBehaviour
	{
		[SerializeField]
		private Health _health = new Health();

		public Health health => _health;

		[SerializeField]
		private CharacterMovement _movement = null;

		private Transform _transform = null;

		public CharacterMovement movement => _movement;

		public new Transform transform => this.GetCachedComponent(ref _transform);

		private void Awake()
		{
			_movement.Initialize(transform);
		}

		private void FixedUpdate()
		{
			_movement.FixedUpdate();
		}
	}
}
