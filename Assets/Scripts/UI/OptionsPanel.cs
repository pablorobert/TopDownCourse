using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsPanel : MonoBehaviour
{
    [Header("Colors")]
    public Color activeColor;
    public Color inactiveColor;

    public Text languageText;

    public List<string> on = new List<string>();
    public List<string> off = new List<string>();

    public List<Button> buttons = new List<Button>();
    public List<Image> backgrounds = new List<Image>();
    public List<string> textos = new List<string>();

    [Header("Sound")]

    public Text bgmOn;

    public Text bgmOff;


    public void ShowUI()
    {
        ShowAllButtons();
        DeselectAll();

        int index = 0;

        switch (DialogueController.Instance.locale)
        {
            case Locales.pt_BR:
                index = 0;
                break;
            case Locales.en_US:
                index = 1;
                break;
            case Locales.es_ES:
                index = 2;
                break;
        }

        languageText.text = textos[index];
        buttons[index].interactable = false;
        backgrounds[index].gameObject.SetActive(true);
        bgmOn.text = on[index];
        bgmOff.text = off[index];

        if (AudioManager.Instance.bgmOn)
        {
            bgmOn.color = activeColor;
            bgmOff.color = inactiveColor;
        } else
        {
            bgmOff.color = activeColor;
            bgmOn.color = inactiveColor;
        }
    }
    private void DeselectAll()
    {
        foreach (Image image in backgrounds)
        {
            image.gameObject.SetActive(false);
        }
    }

    private void ShowAllButtons()
    {
        foreach (Button button in buttons)
        {
            button.interactable = true;
        }
    }

    public void SelectBR()
    {
        DialogueController.Instance.ChangeLocale(Locales.pt_BR);
        ShowUI();
    }

    public void SelectUS()
    {
        DialogueController.Instance.ChangeLocale(Locales.en_US);
        ShowUI();
    }

    public void SelectES()
    {
        DialogueController.Instance.ChangeLocale(Locales.es_ES);
        ShowUI();
    }

    public void SoundOn()
    {
        AudioManager.Instance.SoundOn();
        ShowUI();
    }

    public void SoundOff()
    {
        AudioManager.Instance.SoundOff();
        ShowUI();
    }
}
