using System;
using UnityEngine;

namespace SM
{
    public enum ResourceType
    {
        Food,
        Stone,
        Wood
    }

    public class Resource : MonoBehaviour
    {
        [SerializeField] private ResourceType resourceType;

        public ResourceType ResourceType
        {
            get => resourceType;
            set => resourceType = value;
        }

        [SerializeField] private int initialAmount;

        public int InitialAmount
        {
            get => initialAmount;
            set => initialAmount = value;
        }

        [SerializeField] private int amountAvailable;

        public int AmountAvailable
        {
            get => amountAvailable;
            set => amountAvailable = value;
        }

        public delegate void ResourceExhausted();

        public event ResourceExhausted OnResourceExhausted;

        private void Awake()
        {
            AmountAvailable = InitialAmount;
        }

        public void RemoveAmount(int amountToRemove, NPCController npc)
        {
            bool amountIsAvailable = amountToRemove <= amountAvailable;
            if (amountIsAvailable)  
            {
                npc.Inventory.AddResource(resourceType, amountToRemove);
                AmountAvailable -= amountToRemove;
            }
            else
            {
                npc.Inventory.AddResource(resourceType, AmountAvailable);
                AmountAvailable = 0;
            }

            if (amountAvailable <= 0)
            {
                OnResourceExhausted?.Invoke();
                Destroy(gameObject);
            }
        }
    }
}