using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItems : MonoBehaviour
{
    public int currentWood;
    [SerializeField] private int maxWood;

    public int currentWater;
    [SerializeField] private int maxWater;

    public int carrots;
    [SerializeField] private int maxCarrots;

    public int fishes;
    [SerializeField] private int maxFishes;

    public void AddWater(int water)
    {
        currentWater += water;
        if (currentWater >= maxWater)
        {
            currentWater = maxWater;
        }
    }

    public void AddWood(int wood)
    {
        currentWood += wood;
        if (currentWood >= maxWood)
        {
            currentWood = maxWood;
        }
    }

    public void AddCarrot(int carrot)
    {
        carrots += carrot;
        if (carrots >= maxCarrots)
        {
            carrots = maxCarrots;
        }
    }

    public void AddFish(int fish)
    {
        fishes += fish;
        if (fishes >= maxFishes)
        {
            fishes = maxFishes;
        }
    }

    public bool IsWoodFull()
    {
        return currentWood == maxWood;
    }

    public bool IsCarrotFull()
    {
        return carrots == maxCarrots;
    }

    public bool IsFishFull()
    {
        return fishes == maxFishes;
    }

    public float waterFillAmount()
    {
        if (maxWood == 0)
            return 0;
        return (float)currentWater / (float)maxWater;
    }
    public float woodFillAmount()
    {
        if (maxWood == 0)
            return 0;
        return (float)currentWood / (float)maxWood;
    }

    public float carrotFillAmount()
    {
        if (maxCarrots == 0)
            return 0;
        return (float)carrots / (float)maxCarrots;
    }

    public float fishFillAmount()
    {
        if (maxFishes == 0)
            return 0;
        return (float)fishes / (float)maxFishes;
    }


}
