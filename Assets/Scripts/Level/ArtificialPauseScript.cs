using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtificialPauseScript : MonoBehaviour
{
    //Paralax
    [SerializeField]
    private Background backgroundScript;

    private Background.Layer[] background;

    private Dictionary<GameObject, float> backgroundSpeedValues = new Dictionary<GameObject, float>();

    //Movement
    [Header("MovementScripts")]
    [SerializeField]
    private Movement movementScript;
    [SerializeField]
    private ShipVacoomScript vacoomScript;

    [Header("SpawnersScripts")]

    [SerializeField]
    private GarbageSpawner garbageSpawner;

    [SerializeField]
    private SmallGarbageSpawner smallGarbageSpawner;

    [SerializeField]
    private WoodSpawner woodSpawner;

    private void Awake()
    {
        //storing speed values for pause effect in tutorial
        background = backgroundScript.Layers;
        for (int i = 0; i < background.Length; i++)
        {
            backgroundSpeedValues.Add(background[i].image, backgroundScript[i]);
            backgroundScript[i] = 0;
        }

        DeactivateAllSpawners();
        DeactivateAllMovment();
    }


    private void DeactivateAllSpawners()
    {
        garbageSpawner.enabled = false;
        smallGarbageSpawner.enabled = false;
        woodSpawner.enabled = false;
    }
    private void DeactivateAllMovment()
    {
        vacoomScript.CanUseVacoom = false;
        movementScript.CanMove = false;
    }
}
