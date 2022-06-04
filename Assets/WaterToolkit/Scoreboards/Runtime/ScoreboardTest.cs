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
            Scoreboard scoreboard = new Scoreboard
            {
                duplicateHandling = GameDatabaseDuplicateHandling.Override
            };

            for(int i = 0; i < 10; i++)
            {
                ScoreObject scoreObj = new ScoreObject($"{UnityEngine.Random.Range(0, 99999)}");
                scoreObj.AddValue(ScoreObject.nameKey, $"Player{i}");
                scoreObj.AddValue(ScoreObject.scoreKey, UnityEngine.Random.Range(3, 99));
                scoreObj.AddValue("day", UnityEngine.Random.Range(1, 5));
                scoreboard.Add(scoreObj);
            }

            string result = string.Empty;
            List<ScoreObject> scoreObjs = scoreboard.Get(
                (ScoreObject obj) => obj.IsValid(), 5,
                new GameDatabaseIntValueComparer<ScoreObject>(ScoreObject.scoreKey));

            result += $"Scoreboard Results: {Environment.NewLine}";
            foreach(ScoreObject scoreObj in scoreObjs)
            {
                result += scoreObj.ToString();
            }

            Debug.Log(result);
        }
    }
}

