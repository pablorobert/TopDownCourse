using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class HUDController : MonoBehaviour
{
    private PlayerItems playerItems;
    private PlayerController playerController;

    [Header("Items")]
    public Image waterImageFill;
    public Image woodImageFill;
    public Image carrotImageFill;
    public Image fishImageFill;

    [Header("Tools")]
    public List<Image> toolsUI;
    public Color activeAlpha;
    public Color inactiveAlpha;

    void Awake()
    {
        playerController = FindObjectOfType<PlayerController>();
        playerItems = playerController.GetComponent<PlayerItems>();
    }

    void Start()
    {
        waterImageFill.fillAmount = woodImageFill.fillAmount =
        carrotImageFill.fillAmount = fishImageFill.fillAmount = 0;
    }

    void Update()
    {
        waterImageFill.fillAmount = playerItems.waterFillAmount();
        woodImageFill.fillAmount = playerItems.woodFillAmount();
        carrotImageFill.fillAmount = playerItems.carrotFillAmount();
        fishImageFill.fillAmount = playerItems.fishFillAmount();
    }

    public void UpdateTool()
    {
        for (int i = 0; i < toolsUI.Count; i++)
        {
            toolsUI[i].color = inactiveAlpha;
        }
        toolsUI[(int)playerController.ActiveTool].color = activeAlpha;
    }
}
