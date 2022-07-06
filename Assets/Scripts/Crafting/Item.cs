using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    None,
    Wood,
    Carrot,
    Fish,
    Sword,
}
[CreateAssetMenu(menuName = "Items/New Item", fileName = "New Item")]
public class Item : ScriptableObject
{
    public ItemType type;
    public Sprite sprite;

    public string itemName;

}
