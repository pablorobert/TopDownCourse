using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestCollect3Woods : Quest
{
    public override void OnComplete()
    {
        GameManager.Instance.questManager.OnCompleteQuest(index);
    }

    public override void Check()
    {
        base.Check();

        if (playerItems.currentWood >= 3)
        {
            print("Quest 3 woods concluída");
            isCompleted = true;
            OnComplete();
        }
    }
}
