using UnityEngine;
using WaterToolkit.GameDatabases;

namespace WaterToolkit.Inventories
{
	[CreateAssetMenu(menuName = "WaterToolkit/Inventories/Inventory")]
    public class Inventory : GameCollection<ItemObject> { }
}
