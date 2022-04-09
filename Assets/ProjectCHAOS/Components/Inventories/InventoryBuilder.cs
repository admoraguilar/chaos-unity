using System;
using UnityEngine;

namespace ProjectCHAOS.Inventories
{
    [CreateAssetMenu(menuName = "Playground/Inventory Builder")]
    public class InventoryBuilder : ScriptableObject
    {
        [Serializable]
        public class ItemObjectBuilder
        {
            public string name = string.Empty;
            public int startingQuantity = 0;
            public string category = string.Empty;
            public float rarity = 0f;
            public Sprite sprite = null;

            public ItemObject Build()
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

        [SerializeField]
        private ItemObjectBuilder[] _items = null;

        public ItemObjectBuilder[] items
        {
            get => _items;
        }

        public Inventory Build()
        {
            Inventory inventory = new Inventory();
            foreach(ItemObjectBuilder item in items)
            {
                inventory.Add(item.Build());
            }
            return inventory;
        }
    }
}
