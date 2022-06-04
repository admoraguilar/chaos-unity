using System.Collections.Generic;

namespace WaterToolkit.GameDatabases
{
    public class GameDatabaseIntValueComparer<T> : IComparer<T> where T : GameDatabaseObject
    {
        private string _key = string.Empty;

        public GameDatabaseIntValueComparer(string key)
        {
            _key = key;
        }

        public int Compare(T x, T y)
        {
            int valueX = -1;
            int valueY = -1;

            bool isValueXExists = x.TryGetValue(_key, out valueX);
            bool isValueYExists = y.TryGetValue(_key, out valueY);
        
            if(isValueXExists && !isValueYExists)
            {
                return -1;
            } 
            else if(!isValueXExists && isValueYExists)
            {
                return 1;
            }
            else if(isValueXExists && isValueYExists)
            {
                if(valueX > valueY) { return -1; }
                else if(valueX < valueY) { return 1; }
                else { return 0; }
            } 
            else
            {
                return 0;
            }
        }
    }
}
