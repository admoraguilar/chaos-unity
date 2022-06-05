using System.Collections.Generic;
using UnityEngine;

namespace WaterToolkit.Waves
{
	public abstract class WaveOperator : ScriptableObject
	{
		public abstract void Operate(int index, List<WaveData> dataList);

		protected List<WaveData> CloneWaves(int index, IEnumerable<WaveData> waves)
		{
			List<WaveData> result = new List<WaveData>();
			foreach(WaveData wave in waves) {
				WaveData dataInstance = new WaveData(index, wave);
				result.Add(dataInstance);
			}
			return result;
		}
	}
}

