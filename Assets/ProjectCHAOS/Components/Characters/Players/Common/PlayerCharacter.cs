using UnityEngine;
using ProjectCHAOS;
using ProjectCHAOS.Behave;

namespace ProjectCHAOS.Characters.Players
{
	public class PlayerCharacter : MonoBehaviour, IHealth
	{
		[SerializeField]
		private Health _health = null;

		[SerializeField]
		private CharacterMovement _movement = null;

		[SerializeField]
		private CharacterStats _characterStats = null;

		private Transform _transform = null;

		public Health health => _health;

		public CharacterMovement movement => _movement;

		public CharacterStats characterStats => _characterStats;

		public new Transform transform => this.GetCachedComponent(ref _transform);

		private void Awake()
		{
			_movement.Initialize(transform);
		}

		private void FixedUpdate()
		{
			_movement.FixedUpdate();
		}

#if UNITY_EDITOR

		private void OnValidate()
		{
			characterStats.Editor_OnValidate();
		}

#endif
	}
}
