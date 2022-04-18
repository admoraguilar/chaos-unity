using System;
using System.Collections.Generic;
using ProjectCHAOS.ValueBoards;

namespace ProjectCHAOS.Scoreboards
{
	[Serializable]
    public class ScoreObject : ValueObject
    {
        public const string nameKey = "name";
        public const string scoreKey = "score";
		public const string daysKey = "days";
		public const string wavesKey = "waves";

        public string name
        {
            get => GetValue<string>(nameKey);
            set => SetValue(nameKey, value);
        }

        public int score
        {
            get => GetValue<int>(scoreKey);
            set => SetValue(scoreKey, value);
        }

		public int days
		{
			get => GetValue<int>(daysKey);
			set => SetValue(daysKey, value);
		}

		public int waves
		{
			get => GetValue<int>(wavesKey);
			set => SetValue(wavesKey, value);
		}

		public ScoreObject() : base() { }

        public ScoreObject(string key) : base(key) { }

        public ScoreObject(string key, IDictionary<string, object> values) : base(key, values) { }

		protected override void Initialize(string key = null, IDictionary<string, object> values = null)
		{
			base.Initialize(key, values);

			name = string.Empty;
			score = 0;
		}

		public override bool IsValid()
        {
            return base.IsValid() && 
                (name != string.Empty && score >= 0);
        }
    }
}