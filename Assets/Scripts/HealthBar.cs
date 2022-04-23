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

    private int currentHealth;
    private int maxHealth;

    private PlayerController player;

    void Awake()
    {
        player = FindObjectOfType<PlayerController>();
    }

    void Update()
    {
        currentHealth = player.CurrentHealth;
        maxHealth = player.MaxHealth;
        Draw();
    }

    public void Draw()
    {
        for (int i = 0; i < images.Count; i++)
        {
            if (currentHealth > (i * 2 + 1))
            {
                images[i].sprite = full;
            }
            else if (currentHealth > (i * 2))
            {
                images[i].sprite = half;
            }
            else
            {
                images[i].sprite = empty;
            }
        }
    }
}
