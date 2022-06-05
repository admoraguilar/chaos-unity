using System;
using System.Collections.Generic;
using UnityEngine;
using WaterToolkit.GameDatabases;

namespace WaterToolkit.Scoreboards
{
    public class ScoreboardTest : MonoBehaviour
    {
        private void Start()
        {
            Run();
        }

        public void Run()
        {
            Scoreboard scoreboard = new Scoreboard();

            for(int i = 0; i < 10; i++)
            {
                ScoreItem scoreObj = new ScoreItem();
				scoreObj.name = $"Player{i}";
				scoreObj.score = UnityEngine.Random.Range(3, 99);
				scoreObj.days = UnityEngine.Random.Range(1, 5);
                scoreboard.Add(scoreObj);
            }

            string result = string.Empty;
            List<ScoreItem> scoreObjs = scoreboard.Get(
                (ScoreItem obj) => obj.IsValid(), 5,
                (ScoreItem a, ScoreItem b) => new IntValueComparer().Compare(a.score, b.score));

            result += $"Scoreboard Results: {Environment.NewLine}";
            foreach(ScoreItem scoreObj in scoreObjs)
            {
                result += scoreObj.ToString();
            }

            Debug.Log(result);
        }
    }
}

