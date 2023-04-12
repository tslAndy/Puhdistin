using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtificialPauseScript : MonoBehaviour
{
    //Paralax
    [SerializeField]
    private NewBackground backgroundScript;

    private BackLayer[] background;

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
        //background = backgroundScript.BackLayers;
        

        DeactivateAllSpawners();
        DeactivateAllMovment();
        DeactivateBackground();
    }

    public void DeactivateBackground()
    {
        for (int i = 0; i < background.Length; i++)
        {
            backgroundSpeedValues.Add(background[i].image, background[i].speed);
            background[i].speed = 0;
        }
    }
    public void ActivateBackground()
    {
        /*for (int i = 0; i < background.Length; i++)
        {
            background[i].speed = backgroundSpeedValues[background[i].image];
        }*/
    }
    public void DeactivateAllSpawners()
    {
        garbageSpawner.enabled = false;
        smallGarbageSpawner.enabled = false;
        woodSpawner.enabled = false;
    }
    public void DeactivateAllMovment()
    {
        movementScript.CanMove = false;
        HarpoonInShipState.DisableThrowing();
        vacoomScript.CanUseVacoom = false;
    }
    public void ActivateWoodSpawner() => woodSpawner.enabled = true;
    public void ActivateSmallGarbageSpawner() => smallGarbageSpawner.enabled = true;
    public void ActivateGarbageSpawner() => garbageSpawner.enabled = true;
    public void ActivateVacoom() => vacoomScript.CanUseVacoom = true;
    public void ActivateThroving() => HarpoonInShipState.EnableThrowing();
    public void ActivateShipMovement() => movementScript.CanMove = true;
}
