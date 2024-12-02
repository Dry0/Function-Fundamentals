using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InventorySystem : MonoBehaviour
{
    public struct Item
    {
        public int Id { get; }
        public string Name { get; }
        public float Weight { get; }
        public int Value { get; }
        
        //Constructor
        public Item(int id, string name, float weight, int value)
        {
            Id = id;
            Name = name;
            Weight = weight;
            Value = value;
        }
    }

    public class FixedInventory
    {
        private Item?[] items = new Item?[10]; // Array met nullable Item structs

        public bool AddItem(Item item)
        {
            for (int i = 0; i < items.Length; i++)
            {
                if (items[i] == null) // Controleer op een lege plek
                {
                    items[i] = item; // Voeg het item toe
                    return true;
                }
            }
            return false; // Geen lege plek beschikbaar
        }

        public Item? RemoveItem(int index)
        {
            if (index < 0 || index >= items.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(index), "Index is out of range.");
            }

            Item? removedItem = items[index]; // Haal het item op
            items[index] = null; // Maak de plek leeg
            return removedItem; // Retourneer het verwijderde item
        }

        public int GetTotalValue()
        {
            int totalValue = 0;
            foreach (Item? item in items)
            {
                if (item.HasValue) // Controleer of het item niet null is
                {
                    totalValue += item.Value.Value; // Toegang tot de waarde van de Item
                }
            }
            return totalValue;
        }

        public float GetTotalWeight()
        {
            float totalWeight = 0;
            foreach (Item? item in items)
            {
                if (item.HasValue) // Controleer of het item niet null is
                {
                    totalWeight += item.Value.Weight; // Toegang tot het gewicht van de Item
                }
            }
            return totalWeight;
        }
    }
    
    public class FlexibleInventory
    {
        private List<Item> items = new List<Item>(); // Dynamische lijst van items
    
        public void AddItem(Item item)
        {
            items.Add(item); // Voeg het item toe aan de lijst
        }
    
        public Item RemoveItem(int index)
        {
            if (index < 0 || index >= items.Count)
            {
                throw new ArgumentOutOfRangeException(nameof(index), "Index is out of range.");
            }
    
            Item removedItem = items[index]; // Haal het item op
            items.RemoveAt(index); // Verwijder het item uit de lijst
            return removedItem; // Retourneer het verwijderde item
        }
    
        public int GetTotalValue()
        {
            int totalValue = 0;
            foreach (Item item in items)
            {
                totalValue += item.Value;
            }
            return totalValue;
        }
    
        public float GetTotalWeight()
        {
            float totalWeight = 0;
            foreach (Item item in items)
            {
                totalWeight += item.Weight;
            }
            return totalWeight;
        }
    }
    
    void TestInventories()
    {
        // Maak wat test items
        Item sword = new Item(1, "Sword", 5.0f, 100);
        Item shield = new Item(2, "Shield", 7.5f, 75);

        // Test fixed inventory
        FixedInventory fixedInv = new FixedInventory();
        bool added = fixedInv.AddItem(sword);
        Debug.Log($"Added sword to fixed inventory: {added}");

        // Test flexible inventory
        FlexibleInventory flexInv = new FlexibleInventory();
        flexInv.AddItem(sword);
        flexInv.AddItem(shield);
        //flexInv.SortByValue();

        // Etc...
    }
}
