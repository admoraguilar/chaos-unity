using System;
using System.Collections.Generic;
using ProjectCHAOS.Systems;

namespace ProjectCHAOS.Gameplay.Scoreboards
{
	[Serializable]
    public class ScoreObject : ValueObject
    {
        public const string nameKey = "name";
        public const string scoreKey = "score";

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

        public ScoreObject(string key) : base(key)
        {
            name = string.Empty;
            score = 0;
        }

        public ScoreObject(string key, IDictionary<string, object> values) : base(key, values)
        {
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