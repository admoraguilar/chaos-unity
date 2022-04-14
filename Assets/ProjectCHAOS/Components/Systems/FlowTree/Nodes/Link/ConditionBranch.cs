using System;
using UnityEngine;

namespace ProjectCHAOS.Common
{
	public class ConditionBranch : Branch
	{
		public override string internal_description
		{
			get {
				return "A [Branch] Node." + Environment.NewLine +
					   "When the FlowTree traverses this [Node] it quickly calls Next() " +
					   "If the condition provided on this [Branch] ended up true then the FlowTree " +
					   "moves to the child of this [Node]. If false then the FlowTree skips the " +
					   "entire parent and it's children, then moves to the [Node] right below this.";
			}
		}

		public Condition condition => _condition;
		[SerializeField]
		private Condition _condition = null;

		internal override bool isIncludeInHistory => false;

		public override bool ConditionResult()
		{
			return condition.result;
		}
	}
}
