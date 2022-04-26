using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestCollect1Fish : Quest
{
    public override void OnComplete() //show some UI
    {
        GameManager.Instance.questManager.OnCompleteQuest(index);
    }

    public override void Check()
    {
        base.Check();

        if (playerItems.fishes >= 1)
        {
            isCompleted = true;
            OnComplete();
        }
    }
}
