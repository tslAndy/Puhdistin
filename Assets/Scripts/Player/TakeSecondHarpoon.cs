using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeSecondHarpoon : MonoBehaviour
{
    [SerializeField] private GameObject[] toActivate;

    private void Start()
    {
        AnotherShop.onSecondHarpoonBuy += Take;
    }

    void Take()
    {
        foreach (GameObject obj in toActivate)
        {
            obj.SetActive(true);
        }
    }
}
