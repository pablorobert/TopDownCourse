using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Recipes/New Recipe", fileName = "New Recipe")]
public class Recipe : ScriptableObject
{
    public Item[] items = new Item[9];

    public Item item_00;
    public Item item_01;
    public Item item_02;
    public Item item_10;
    public Item item_11;
    public Item item_12;
    public Item item_20;
    public Item item_21;
    public Item item_22;

    public Item GetItem(int x, int y)
    {
        return items[y * 3 + x];
    }

    public Item GetItem(int pos)
    {
        return items[pos];
    }

    public Item getItem(int x)
    {
        //return items[x * 3 + y];
        /*if (x == 0) return item_00;
        if (x == 1) return item_01;
        if (x == 2) return item_02;

        if (x == 3) return item_10;
        if (x == 4) return item_11;
        if (x == 5) return item_12;

        if (x == 6) return item_20;
        if (x == 7) return item_21;
        if (x == 8) return item_22;*/

        /*if (x == 0) return item_02;
        if (x == 1) return item_12;
        if (x == 2) return item_22;

        if (x == 3) return item_01;
        if (x == 4) return item_11;
        if (x == 5) return item_21;

        if (x == 6) return item_00;
        if (x == 7) return item_10;
        if (x == 8) return item_20;*/

        return null;
    }

    public bool Check(List<Item> items)
    {
        int index = 0;
        foreach (Item item in items)
        {
            if (item != GetItem(index))
            {
                return false;
            }
            index++;
        }
        //Debug.Log("Success");
        return true;
    }

    public Item outcome;
}
