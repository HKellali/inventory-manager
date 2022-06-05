using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory
{
    private Item[] items;

    public Inventory(int size)
    {
        this.items = new Item[size];
    }

    public void AddItem(Item item, int index)
    {
        items[index] = item;
    }

    public void Removetem(int index)
    {
        items[index] = null;
    }

    public void SwapItems(int index1, int index2)
    {
        Item temp = items[index1];
        items[index1] = items[index2];
        items[index2] = temp;
    }

    public Item[] GetItems()
    {
        return this.items;
    }
}
