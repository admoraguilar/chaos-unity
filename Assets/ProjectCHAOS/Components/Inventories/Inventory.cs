using System;
using System.Collections.Generic;
using ProjectCHAOS.BaseClasses;
using UnityEngine;

namespace ProjectCHAOS.Inventories
{
    [Serializable]
    public class Inventory : ValueBoard<ItemObject> { }

    [Serializable]
    public class ItemObject : ValueObject
    {
        public const string nameKey = "name";
        public const string quantityKey = "quantity";
        public const string categoryKey = "category";
        public const string rarityKey = "rarity";
        public const string spriteKey = "spriteKey";
        
        public string name
        {
            get => GetValue<string>(nameKey);
            set => SetValue(nameKey, value);
        }

        public int quantity
        {
            get => GetValue<int>(quantityKey);
            set => SetValue(quantityKey, value);
        }

        public string category
        {
            get => GetValue<string>(categoryKey);
            set => SetValue(categoryKey, value);
        }

        public float rarity
        {
            get => GetValue<float>(rarityKey);
            set => SetValue(rarityKey, value);
        }

        public Sprite sprite
        {
            get => GetValue<Sprite>(spriteKey);
            set => SetValue(spriteKey, value);
        }

        public ItemObject(string key) : base(key)
        {
            name = string.Empty;
            quantity = 0;
            category = "Default";
            rarity = 0f;
            sprite = null;
        }

        public ItemObject(string key, IDictionary<string, object> values) : base(key, values)
        {
            name = string.Empty;
            quantity = 0;
            category = "Default";
            rarity = 0f;
            sprite = null;
        }

        public override bool IsValid()
        {
            return base.IsValid() && 
                (name != string.Empty && quantity >= 0 && 
                category != string.Empty && rarity >= 0f);
        }
    }
}
