using System;
using UnityEngine;
using ProjectCHAOS.Systems.FlowTrees;
using ProjectCHAOS.Gameplay.Characters;

namespace ProjectCHAOS.Gameplay.GameModes.Survival
{
	[Serializable]
	public class SurvivalFlow
	{
		[SerializeField]
		private FlowTree _tree = null;

		[Header("Nodes")]
		[SerializeField]
		private Node _base = null;

		[SerializeField]
		private Node _outside = null;

		[SerializeField]
		private Node _outsideDead = null;

		[SerializeField]
		private Node _reload = null;

		private SurvivalWorld _world = null;

		public FlowTree tree => _tree;

		public Node @base => _base;

		public Node outside => _outside;

		public Node outsideDead => _outsideDead;

		public Node reload => _reload;

		public SurvivalWorld world
		{
			get => _world;
			private set => _world = value;
		}

		private void OnBaseVisit()
		{

		}

		private void OnOutsideVisit()
		{
			world.OnOutsideVisit();
		}

		private void OnOutsideDeadVisit()
		{
			world.OnOutsideDeadVisit();
			outsideDead.Next();
		}

		private void OnReloadVisit()
		{

		}

		private void OnCharacterBasePortalEnter(CharacterMechanic characterMechanic)
		{
			@base.Next();
		}

		private void OnPlayerCharacterHealthEmpty()
		{
			outside.Next();
		}

		public void Awake(SurvivalWorld world)
		{
			this.world = world;
		}

		public void OnEnable()
		{
			@base.OnVisit += OnBaseVisit;
			outside.OnVisit += OnOutsideVisit;
			outsideDead.OnVisit += OnOutsideDeadVisit;
			reload.OnVisit += OnReloadVisit;

			world.OnCharacterBasePortalEnter += OnCharacterBasePortalEnter;
			world.OnPlayerCharacterHealthEmpty += OnPlayerCharacterHealthEmpty;
		}

		public void OnDisable()
		{
			@base.OnVisit -= OnBaseVisit;
			outside.OnVisit -= OnOutsideVisit;
			outsideDead.OnVisit -= OnOutsideDeadVisit;
			reload.OnVisit -= OnReloadVisit;

			world.OnCharacterBasePortalEnter -= OnCharacterBasePortalEnter;
			world.OnPlayerCharacterHealthEmpty -= OnPlayerCharacterHealthEmpty;
		}
	}
}
