using System.Collections.Generic;

namespace WaterToolkit.GameDatabases
{
    public class IntValueComparer : IComparer<int>
    {
        public int Compare(int x, int y)
        {
            if(x > y) { return -1; }
			else if(x < y) { return 1; }
			else { return 0; }
        }
    }
}
