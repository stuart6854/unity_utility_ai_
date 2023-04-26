using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace SM
{
    public class StorageInventory : MonoBehaviour
    {
        public int MaxCapacity { get; protected set; }
        public Dictionary<ResourceType, int> Inventory { get; protected set; }

        public virtual void InitialiseInventory()
        {
            Inventory = new Dictionary<ResourceType, int>()
            {
                { ResourceType.Food, 0 },
                { ResourceType.Stone, 0 },
                { ResourceType.Wood, 0 }
            };
        }

        public virtual void AddResource(ResourceType r, int amount)
        {
        }

        public virtual void RemoveResource(ResourceType type, int amount)
        {
        }

        public virtual int CheckInventoryCount()
        {
            return Inventory.Keys.Sum(type => Inventory[type]);
        }

        public virtual bool DoesInventoryHaveItems()
        {
            return Inventory.Keys.Any(type => Inventory[type] > 0);
        }

        public virtual float HowFullIsInventory()
        {
            float total = CheckInventoryCount();
            return total / MaxCapacity;
        }
    }
}