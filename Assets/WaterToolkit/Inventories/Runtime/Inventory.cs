using System;
using WaterToolkit.GameDatabases;

namespace WaterToolkit.Inventories
{
	[Serializable]
    public class Inventory : GameDatabaseCollection<ItemObject> { }
}
