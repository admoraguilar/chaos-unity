using UnityEngine;
using WaterToolkit;
using WaterToolkit.Behave;
using ProjectCHAOS.Characters;

namespace ProjectCHAOS.Characters.Players
{
	public class PlayerCharacter : MonoBehaviour, IHealth, ICharacterStats
	{
		[SerializeField]
		private Health _health = null;

		[SerializeField]
		private EntityMovement _movement = null;

		[SerializeField]
		private CharacterStats _characterStats = null;

		private Transform _transform = null;
		private CharacterController _characterController = null;
		private Rigidbody _rigidbody = null;

		public Health health => _health;

		public EntityMovement movement => _movement;

		public CharacterStats characterStats => _characterStats;

		public new Transform transform => this.GetCachedComponent(ref _transform);

		public CharacterController characterController => this.GetCachedComponent(ref _characterController);

		public new Rigidbody rigidbody => this.GetCachedComponent(ref _rigidbody);

		private void Awake()
		{
			_movement.Initialize(transform, characterController, rigidbody);
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
