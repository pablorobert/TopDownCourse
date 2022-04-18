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


}
