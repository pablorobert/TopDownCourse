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

    public Item getItem(int x, int y)
    {
        //return items[x * 3 + y];
        if (x == 0 && y == 0) return item_00;
        if (x == 0 && y == 1) return item_01;
        if (x == 0 && y == 2) return item_02;

        if (x == 1 && y == 0) return item_10;
        if (x == 1 && y == 1) return item_11;
        if (x == 1 && y == 2) return item_12;

        if (x == 2 && y == 0) return item_20;
        if (x == 2 && y == 1) return item_21;
        if (x == 2 && y == 2) return item_22;

        return null;
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

        if (x == 0) return item_02;
        if (x == 1) return item_12;
        if (x == 2) return item_22;

        if (x == 3) return item_01;
        if (x == 4) return item_11;
        if (x == 5) return item_21;

        if (x == 6) return item_00;
        if (x == 7) return item_10;
        if (x == 8) return item_20;

        return null;
    }

    public bool Check(List<Item> items)
    {
        int index = -1;

        foreach (Item item in items)
        {
            index++;
            /*Debug.Log(index);
            Debug.Log(item);
            Debug.Log(getItem(index));*/

            /*if (item == null && getItem(index) != null)
            {
                Debug.Log("Falhou");
                return false;
            }*/
            if (item != getItem(index))
            {
                //Debug.Log("Failed");
                return false;
            }
        }
        Debug.Log("Sucess");
        return true;
    }

    public Item outcome;
}
