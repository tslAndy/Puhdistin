using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipVacoomScript : MonoBehaviour
{
    [SerializeField]
    private AreaEffector2D areaEffector;

    [SerializeField]
    private PolygonCollider2D collider;

    [SerializeField]
    private ParticleSystem[] effects;

    public OnGarbageCollecting onGarbageCollecting;


    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
       if(Input.GetKey(KeyCode.Space))
        {
            ActivateAreaEffector();
            foreach (var effect in effects)
            {
                if(!effect.isPlaying) effect.Play();
            }
        } else
        {
            DeactivateAreaEffector();
            foreach (var effect in effects)
            {
                if (effect.isPlaying) effect.Stop();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("SmallGarbage"))
        {
            onGarbageCollecting.HandleCollect(collision.gameObject.tag, collision.gameObject);
            Destroy(collision.gameObject);
        }
    }
    private void ActivateAreaEffector()
    {
        Debug.Log("Activated");
        areaEffector.enabled = true;
    }
    private void DeactivateAreaEffector()
    {
        areaEffector.enabled = false;      
    }

}
