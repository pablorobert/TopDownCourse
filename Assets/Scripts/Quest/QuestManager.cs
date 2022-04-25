using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{

    public List<Quest> quests = new List<Quest>();

    void Update()
    {
        foreach (Quest quest in quests)
        {
            if (!quest.isCompleted)
            {
                quest.Check();
            }
        }
    }

    //event
    public void OnCompleteQuest(int index)
    {
        //...
    }
}
