using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreAddingEffectScript : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI scoreEffectText;

    private void Start()
    {
        OnGarbageCollecting onGarbageCollecting = GameObject.Find("Level").GetComponent<OnGarbageCollecting>();
        onGarbageCollecting.OnGarbageColldected += ActivateEffect;
    }
    public void ActivateEffect(int garbageValue)
    {
        Debug.Log("Working");
        scoreEffectText.text = $"+ {garbageValue}";
        scoreEffectText.enabled = true;
    }
    public void DeactivateEffect()
    {
        scoreEffectText.enabled = false;
    }
}
