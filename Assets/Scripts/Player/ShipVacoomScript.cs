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

    private bool canUseVacoom = false;

    public bool CanUseVacoom { set { canUseVacoom = value; } }


    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
       if (Input.GetKey(KeyCode.Space) && canUseVacoom)
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
        Debug.Log(collision.gameObject.layer);
        Debug.Log(LayerMask.NameToLayer("SmallGarbage"));
        if (collision.gameObject.CompareTag("SmallGarbage"))
        {
            Debug.Log("WORKINGINSIDE");

            onGarbageCollecting.HandleCollect(collision.gameObject.tag, collision.gameObject);
            Destroy(collision.gameObject);
        }
    }
    private void ActivateAreaEffector()
    {
        areaEffector.enabled = true;
    }
    private void DeactivateAreaEffector()
    {
        areaEffector.enabled = false;      
    }

}
