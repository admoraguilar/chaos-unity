
namespace ProjectCHAOS.FlowTrees
{
	public abstract class Branch : Link
	{
		internal override bool isIncludeInHistory => false;

		public abstract bool ConditionResult();

		protected override void OnDoVisit()
		{
			Next();
		}

		public override void Next()
		{
			if(!ConditionResult() || transform.childCount == 0) {
				base.Next();
				return;
			}

			Node next = transform.GetChild(0).GetComponent<Node>();
			tree.Swap(next);
 		}
	}
}
