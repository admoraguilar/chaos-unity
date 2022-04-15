using ProjectCHAOS.Systems;
using System;
using UnityEngine;

namespace ProjectCHAOS.Gameplay.Inventories
{
    [CreateAssetMenu(menuName = "ProjectCHAOS/Inventory Builder")]
    public class InventoryBuilder : 
		ValueBoardBuilder<
			Inventory, ItemObject, 
			InventoryBuilder.ItemObjectBuilder>
    {
        [Serializable]
        public class ItemObjectBuilder : ValueObjectBuilder<ItemObject>
        {
            public string name = string.Empty;
            public int startingQuantity = 0;
            public string category = string.Empty;
            public float rarity = 0f;
            public Sprite sprite = null;

            public override ItemObject Build()
            {
                ItemObject itemObj = new ItemObject($"{UnityEngine.Random.Range(0, 99999)}");
                itemObj.name = name;
                itemObj.quantity = startingQuantity;
                itemObj.category = category;
                itemObj.rarity = rarity;
                itemObj.sprite = sprite;
                return itemObj;
            }
        }
    }
}
