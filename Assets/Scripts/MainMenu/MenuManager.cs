using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public Animator leftButtonsAnimator;
    public Animator quotesAnimator;
    public Animator settingsAnimator;

    private GameObject settings;

    private void Start()
    {
        if(GameObject.Find("Settings") != null)
        {
            settings = GameObject.Find("Settings");
            DeactivateSettings();
        }
    }

    private void OnLevelWasLoaded()
    {
        Time.timeScale = 1;
    }
    public void FadeToSettings()
    {
        ActivateSettings();
        leftButtonsAnimator.SetTrigger("ButtonsOut");
        quotesAnimator.SetTrigger("QuotesOut");
        settingsAnimator.SetTrigger("SettingsIn");
    }

    public void FadeToMenu()
    {
        leftButtonsAnimator.SetTrigger("ButtonsIn");
        quotesAnimator.SetTrigger("QuotesIn");
        settingsAnimator.SetTrigger("SettingsOut");
    }

    public void ActivateSettings()
    {
        settings.SetActive(true);
    }

    public void DeactivateSettings()
    {
        settings.SetActive(false);
    }
}
