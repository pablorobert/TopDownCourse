using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance; //singleton and dont destroy

    public PlayerController player = null;

    public QuestManager questManager;

    public GameObject chooseLanguagePanel;

    public QuestManager QuestManager
    {
        get { return questManager; }
    }

    //public Sprite[] recipeSprites;
    public Sprite transparentSprite;
    public Item[] recipeItems;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public PlayerController GetPlayer()
    {
        if (player == null)
        {
            player = FindObjectOfType<PlayerController>();
        }
        return player;
    }

    public void ShowLanguagePanel()
    {
        chooseLanguagePanel.SetActive(!chooseLanguagePanel.activeSelf);
        chooseLanguagePanel.GetComponentInChildren<OptionsPanel>().ShowUI();
    }


}
