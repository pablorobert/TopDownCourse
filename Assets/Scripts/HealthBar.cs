using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public List<Image> images;

    public Sprite empty;
    public Sprite half;

    public Sprite full;

    public int currentHealth;
    public int maxHealth;

    void Start()
    {
        Draw();
    }

    void Update()
    {
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        if (currentHealth < 0)
        {
            currentHealth = 0;
        }
        Draw();
    }

    public void Draw()
    {
        for (int i = 0; i < images.Count; i++)
        {
            images[i].sprite = empty;
        }

        int count = ((int)Mathf.Ceil(currentHealth)) / 2;
        int diff = currentHealth - (count * 2);

        for (int i = 0; i < count; i++)
        {
            images[i].sprite = full;
        }

        if (diff == 1)
        {
            images[count].sprite = half;
        }
    }
}
