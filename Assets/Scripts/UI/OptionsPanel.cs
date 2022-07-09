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

    /*public Button ptBR;

    public Button enUS;

    public Button esES;*/
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
            /*case Locales.pt_BR:
                languageText.text = "Escolha o idioma";
                ptBR.interactable = true;
                backgrounds[0].gameObject.SetActive(true);
                break;
            case Locales.en_US:
                languageText.text = "Choose language";
                enUS.interactable = true;
                backgrounds[1].gameObject.SetActive(true);
                break;
            case Locales.es_ES:
                languageText.text = "Seleccione el idioma";
                esES.interactable = true;
                backgrounds[2].gameObject.SetActive(true);
                break;*/
            case Locales.pt_BR:
                index = 0;
                //ptBR.interactable = false;
                break;
            case Locales.en_US:
                index = 1;
                //enUS.interactable = false;
                break;
            case Locales.es_ES:
                index = 2;
                //esES.interactable = true;
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
        /*ptBR.interactable = true;
        enUS.interactable = true;
        esES.interactable = true;*/
        foreach (Button button in buttons)
        {
            button.interactable = true;
        }
    }

    public void SelectBR()
    {
        DialogueController.Instance.locale = Locales.pt_BR;
        ShowUI();
    }

    public void SelectUS()
    {
        DialogueController.Instance.locale = Locales.en_US;
        ShowUI();
    }

    public void SelectES()
    {
        DialogueController.Instance.locale = Locales.es_ES;
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
