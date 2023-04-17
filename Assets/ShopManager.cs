using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    [SerializeField] TMP_Text scoreText;
    [SerializeField] List<Button> upgradeButtons = new List<Button>();
    [SerializeField] HarpoonUpgrades nextUpgrade;

    private static HarpoonUpgrades _currentUpgrade;

    public enum HarpoonUpgrades
    {
        HarpoonWith1,
    }
    void Start()
    {
        TotalScoreScript.totalPlayerScore = 1500; //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        UpdatePoints();
        _currentUpgrade = HarpoonUpgrades.HarpoonWith1;
    }

    // Update is called once per frame
    void Update()
    {
        DeactivateAllButtons();
        switch (_currentUpgrade)
        {
            case HarpoonUpgrades.HarpoonWith1:
                upgradeButtons[0].enabled = true;
                break;
        }
    }

    public void UpdatePoints()
    {
        scoreText.text = TotalScoreScript.totalPlayerScore + "$";
    }
    public void OnUpgradeButtonClick(int cost)
    {
        if (CanBuyUpgrade(cost))
        {
            Debug.Log("Pressed");
            TotalScoreScript.totalPlayerScore -= cost;
            UpdatePoints();
            _currentUpgrade = nextUpgrade;
        }
    }

    private bool CanBuyUpgrade(int uprgadeCost)
    {
        return TotalScoreScript.totalPlayerScore >= uprgadeCost;
    }
    
    private void DeactivateAllButtons()
    {
        foreach (var upgradeButton in upgradeButtons)
        {
            if(upgradeButton.enabled != false)
                upgradeButton.enabled = false;
        }
    }

}