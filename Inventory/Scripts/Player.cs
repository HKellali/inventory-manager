using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Player : MonoBehaviour
{
    public Inventory inventory;
    public InventoryUI inventoryUI;
    /*public Item.ItemType itemType;

    void Awake()
    {
        GenerateInventory();
    }

    void GenerateInventory()
    {
        this.inventory = new Inventory();
        inventoryUI.SetInventory(inventory); 
    }

    void RandomizeInventory()
    {
        itemType = (Item.ItemType)Enum.ToObject(typeof(Item.ItemType), UnityEngine.Random.Range(0, 2));
        inventory.AddItem(new Item { itemType = itemType, amount = 1 });
        inventory.AddItem(new Item { itemType = Item.ItemType.Axe, amount = 1 });
    }*/
}
