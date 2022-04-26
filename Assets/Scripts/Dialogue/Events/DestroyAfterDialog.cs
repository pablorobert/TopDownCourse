using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterDialog : MonoBehaviour
{
    public GameObject source; //item to be destroyed
    public float timeToDestroy = 0.5f;

    public DialogueSettings settings;

    void Start()
    {
        if (source == null)
            source = this.gameObject;
    }

    public void Kill()
    {
        StartCoroutine(DestroyCoroutine());
    }

    IEnumerator DestroyCoroutine()
    {
        yield return new WaitForSeconds(timeToDestroy);
        //destroy this one
        gameObject.SetActive(false);
        Destroy(gameObject, timeToDestroy);
    }

    public void OnEnable()
    {
        settings.OnComplete += Kill;
    }

    public void OnDisable()
    {
        settings.OnComplete -= Kill;
    }
}
