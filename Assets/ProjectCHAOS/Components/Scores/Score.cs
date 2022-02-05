using System;
using UnityEngine;

namespace ProjectCHAOS.Scores
{
	[Serializable]
	public class Score
	{
		public event Action<int> OnUpdate = delegate { };
		public event Action<int> OnNewBest = delegate { };

		[SerializeField]
		private int _id = 0;

		[SerializeField]
		private int _current = 0;

		[SerializeField]
		private int _best = 0;

		public int id
		{
			get => _id;
			private set => _id = value;
		}

		public int current
		{
			get => _current;
			set {
				_current = value;
				OnUpdate(_current);

				if(_current > best) {
					best = value;
					OnNewBest(best);
				}
			}
		}

		public int best
		{
			get => _best;
			private set => _best = value;
		}

		public Score(int id)
		{
			this.id = id;
		}
	}
}
