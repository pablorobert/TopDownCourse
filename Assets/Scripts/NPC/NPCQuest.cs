using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCQuest : MonoBehaviour
{
    public int questIndex;

    public bool isActive;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void StartQuest()
    {
        isActive = true;
    }
}
