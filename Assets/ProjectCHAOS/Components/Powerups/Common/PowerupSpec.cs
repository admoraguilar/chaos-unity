using System.Collections.Generic;
using UnityEngine;

namespace ProjectCHAOS.Powerups
{
	public abstract class PowerupSpec : ScriptableObject, IFlyweightObject<PowerupSpec>
	{
		private ReferenceBag _references = new ReferenceBag();

		public int id => GetInstanceID();

		protected ReferenceBag references => _references;

		public void Initialize(IEnumerable<object> references)
		{
			this.references.AddRange(references);
		}

		public abstract void Use();

		public abstract PowerupSpec Clone();

		protected PowerupSpec FinishClone(PowerupSpec instance)
		{
			instance._references = _references.Clone();
			return instance;
		}
	}
}
