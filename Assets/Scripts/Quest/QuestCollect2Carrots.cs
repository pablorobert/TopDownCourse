using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestCollect2Carrots : Quest
{
    public override void OnComplete()
    {
        //maybe show some UI
        GameManager.Instance.questManager.OnCompleteQuest(index);
    }

    public override void Check()
    {
        base.Check();

        if (playerItems.carrots >= 2)
        {
            isCompleted = true;
            OnComplete();
        }
    }
}
