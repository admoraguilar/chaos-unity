using UnityEngine;
using WaterToolkit;
using WaterToolkit.Behave;
using WaterToolkit.Worlds;

namespace ProjectCHAOS.Characters.AIs
{
	public class BasicAI : MonoBehaviour, IHealth
	{
		[SerializeField]
		private Health _health = null;

		[SerializeField]
		private float _speed = 10f;

		[SerializeField]
		private bool _isParent = false;

		private SimpleMovement _movement = new SimpleMovement();
		private Positioning _positioning = new Positioning();
		private Targetting _targetting = new Targetting();

		[SerializeField]
		private LevelArea _levelArea = null;

		private Transform _transform = null;

		public Health health => _health;

		public SimpleMovement movement => _movement;

		public Positioning positioning => _positioning;

		public Targetting targetting => _targetting;

		public LevelArea levelArea
		{
			get => _levelArea;
			private set => _levelArea = value;
		}

		public new Transform transform => this.GetCachedComponent(ref _transform);

		public void Initialize(LevelArea levelArea)
		{
			this.levelArea = levelArea;

			positioning.Initialize(levelArea);
			movement.Initialize(transform);
			targetting.Initialize(transform);

			Reposition();
		}

		public void Reposition()
		{
			if(!positioning.isFunctional || !targetting.isFunctional ||
			   !movement.isFunctional) { return; }

			targetting.targetPoint = positioning.GetPosition();

			movement.space = Space.World;
			movement.speed = _speed;
			movement.direction = targetting.GetDirectionToTarget();
		}

		private void Start()
		{
			if(levelArea != null) { 
				Initialize(levelArea); 
			}
		}

		private void FixedUpdate()
		{
			if(!targetting.isFunctional || !movement.isFunctional) { return; }

			if(targetting.GetDistanceToTarget() > 0.5f) {
				movement.Update();
			}

			if(_isParent) {
				if(transform.childCount <= 0) {
					Destroy(gameObject);
				}
			}
		}

#if UNITY_EDITOR

		private void OnDrawGizmos()
		{
			if(Application.isPlaying) {
				Gizmos.color = Color.yellow;
				Gizmos.DrawSphere(targetting.targetPoint, 1f);
				Gizmos.DrawRay(transform.position, movement.direction);
			}
		}

#endif
	}
}