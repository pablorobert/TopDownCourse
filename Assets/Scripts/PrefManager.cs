using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefManager : MonoBehaviour
{
    public const string PLAYER_PREF_SOUND = "bgm_sound";
    public const string PLAYER_PREF_LANGUAGE = "locale";

    public static bool GetSound()
    {
        if (PlayerPrefs.HasKey(PLAYER_PREF_SOUND)) {
            return PlayerPrefs.GetInt(PLAYER_PREF_SOUND) == 1;
        } else
        {
            return true;
        }
    }

    public static void SetSound(bool bgmsound)
    {
        PlayerPrefs.SetInt(PLAYER_PREF_SOUND, bgmsound ? 1: 0);
    }

    public static Locales GetLanguage()
    {
        if (PlayerPrefs.HasKey(PLAYER_PREF_LANGUAGE))
        {
            return (Locales) PlayerPrefs.GetInt(PLAYER_PREF_LANGUAGE);
        } else
        {
            return Locales.pt_BR; //default
        }
    }

    public static void SetLanguage(Locales locale)
    {
        PlayerPrefs.SetInt(PLAYER_PREF_LANGUAGE, (int)locale);
    }

  
}
