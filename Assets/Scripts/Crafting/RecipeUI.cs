using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RecipeUI : MonoBehaviour, IPointerDownHandler
{
    public Image itemSprite;

    public Item item;

    private int currentIndex;

    private void Start()
    {
        Reset();
    }

    void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
    {
        currentIndex = (currentIndex + 1) % GameManager.Instance.recipeItems.Length;
        ChangeItem(currentIndex);
    }

    public void Reset()
    {
        ChangeItem(0);
    }

    private void ChangeItem(int index)
    {
        item = GameManager.Instance.recipeItems[currentIndex];
        if (item)
        {
            itemSprite.sprite = item.sprite;
        }
        else
        {
            itemSprite.sprite = GameManager.Instance.transparentSprite;
        }
    }
}
