using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance; //singleton and dont destroy

    public PlayerController player = null;

    public QuestManager questManager;

    public QuestManager QuestManager
    {
        get { return questManager; }
    }

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


}
