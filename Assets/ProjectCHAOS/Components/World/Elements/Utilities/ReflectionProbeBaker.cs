using UnityEngine;
using ProjectCHAOS;

namespace ProjectCHAOS.Worlds
{
	public class ReflectionProbeBaker : MonoBehaviour
	{
		private ReflectionProbe _reflectionProbe = null;

		public ReflectionProbe reflectionProbe => this.GetCachedComponent(ref _reflectionProbe);

		private void Update()
		{
			reflectionProbe.RenderProbe();
		}
	}
}
