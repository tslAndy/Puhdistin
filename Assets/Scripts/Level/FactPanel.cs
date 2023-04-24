using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FactPanel : MonoBehaviour
{
    [SerializeField] private TMP_Text factText;
    [SerializeField] private GameObject panel;

    private void Update()
    {
        if (factText.text != "" && !panel.activeSelf)
            panel.SetActive(true);
        else if (factText.text == "" && panel.activeSelf)
            panel.SetActive(false);
    }
}
