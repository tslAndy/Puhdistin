using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public Animator leftButtonsAnimator;
    public Animator quotesAnimator;
    public Animator settingsAnimator;

    public void FadeToSettings()
    {
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
}
