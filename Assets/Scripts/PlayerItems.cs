using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItems : MonoBehaviour
{
    public int totalWood;
    [SerializeField] private int maxWood;

    public int currentWater;
    [SerializeField] private int maxWater;

    public int carrots;

    public void addWater(int water)
    {
        currentWater += water;
        if (currentWater >= maxWater)
        {
            currentWater = maxWater;
        }
    }

    public void addWood(int wood)
    {
        totalWood += wood;
        if (totalWood >= maxWood)
        {
            totalWood = maxWood;
        }
    }


}
