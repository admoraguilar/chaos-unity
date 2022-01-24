using System;

namespace ProjectCHAOS.Common
{
	public class Teleport : Link
	{
		public override string internal_description
		{
			get {
				return "A [Link] Node." + Environment.NewLine +
					   "Calling Next() on this will result to the FlowTree moving to the " +
					   "[Node] assigned to its 'teleportTo' property.";
			}
		}

		public Node teleportTo = null;

		internal override bool isIncludeInHistory => false;

		protected override void OnDoVisit()
		{
			Next();
		}

		public override void Next()
		{
			tree.Swap(teleportTo);
		}
	}
}
