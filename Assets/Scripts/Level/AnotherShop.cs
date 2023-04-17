using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AnotherShop : MonoBehaviour
{
    [SerializeField] private OnGarbageCollecting onGarbageCollecting;
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private GameObject[] objectsToActivate;

    public delegate void OnSecondHarpoonBuy();
    public static OnSecondHarpoonBuy onSecondHarpoonBuy;

    private bool _upgradeBought;
    public int harpoonPrice;

    public void BuyHarpoon()
    {
        if (onGarbageCollecting.score < harpoonPrice || _upgradeBought)
            return;

        onGarbageCollecting.score -= harpoonPrice;
        scoreText.SetText($"Score: {onGarbageCollecting.score}");
        _upgradeBought = true;

        foreach (GameObject obj in objectsToActivate)
            obj.SetActive(true);
    }
}
