using System.Linq;
using UnityEngine;

namespace SM
{
    public class NPCInventory : StorageInventory
    {
        [SerializeField] private int maxCapacity;
        [SerializeField] private Billboard inventoryUI;

        public delegate void InventoryChangedHandler();

        public event InventoryChangedHandler OnInventoryChanged;

        private void Awake()
        {
            InitialiseInventory();
            SetMaxCapacity(maxCapacity);
        }

        private void OnEnable()
        {
            OnInventoryChanged += InventoryChanged;
        }

        private void OnDisable()
        {
            OnInventoryChanged -= InventoryChanged;
        }

        public void SetMaxCapacity(int capacity)
        {
            MaxCapacity = capacity;
        }

        public void SetUI(Billboard billboard)
        {
            inventoryUI = billboard;
        }

        public override void AddResource(ResourceType r, int amount)
        {
            int amountInInventory = CheckInventoryCount();
            if (amountInInventory + amount > MaxCapacity)
            {
                int amountCanAdd = MaxCapacity - amountInInventory;
                Inventory[r] += amountCanAdd;
            }
            else
            {
                Inventory[r] += amount;
            }

            OnInventoryChanged?.Invoke();
        }

        public void RemoveAllResources()
        {
            var types = Inventory.Keys.ToList();

            foreach (var r in types)
                Inventory[r] = 0;
            
            OnInventoryChanged?.Invoke();
        }

        public override void RemoveResource(ResourceType r, int amount)
        {
            if (Inventory[r] - amount < 0)
                Inventory[r] = 0;
            else
                Inventory[r] -= amount;
            
            OnInventoryChanged?.Invoke();
        }

        private void InventoryChanged()
        {
            inventoryUI.UpdateInventoryText(Inventory[ResourceType.Wood], Inventory[ResourceType.Stone], Inventory[ResourceType.Food]);
        }
    }
}