using System;
using UnityEngine;

namespace WaterToolkit.FlowTrees
{
	public class MultiConditionBranch : Branch
	{
		public override string internal_description
		{
			get {
				return "A [Branch] Node." + Environment.NewLine +
					   "When the FlowTree traverses this [Node] it quickly calls Next() " +
					   "If all the conditions provided on this [Branch] ended up true then the FlowTree " +
					   "moves to the child of this [Node]. If false then the FlowTree skips the " +
					   "entire parent and it's children, then moves to the [Node] right below this.";
			}
		}

		public enum Type
		{
			AND,
			OR
		};

		public Type type => _type;
		[SerializeField]
		private Type _type = Type.AND;

		public Condition[] conditions => _conditions;
		[SerializeField]
		private Condition[] _conditions = null;

		public override bool ConditionResult()
		{
			bool result = false;

			if(conditions.Length > 0) {
				switch(type) {
					case Type.AND:
						result = true;
						foreach(Condition condition in conditions) {
							if(!condition.result) {
								result = false;
								break;
							}
						}
						break;
					case Type.OR:
						foreach(Condition condition in conditions) {
							if(condition.result) {
								result = true;
								break;
							}
						}
						break;
				}
			}

			return result;
		}
	}
}
