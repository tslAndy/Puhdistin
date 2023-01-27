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
    private ContactFilter2D filter;

    [Header("Y, when objects velocity sets to 0")]
    [SerializeField]
    private float minY;

    [Header("Y, when garbage consumed by ship and adds points to player")]
    [SerializeField]
    private float maxY;

    public OnGarbageCollecting onGarbageCollecting;

    private List<Collider2D> colliders = new List<Collider2D>();

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        //Gets all object in colliders range
       collider.OverlapCollider(filter, colliders);
       if(Input.GetKey(KeyCode.Space))
        {
            ActivateAreaEffector();
        } else
        {
            DeactivateAreaEffector();
        }
    }

    private void ActivateAreaEffector()
    {
        areaEffector.enabled = true;
        foreach(Collider2D collider in colliders)
        {          
            if (collider.gameObject.transform.position.y > maxY)
            {
                onGarbageCollecting.HandleCollect(collider.gameObject.tag);
                Destroy(collider.gameObject);
            }

        }
    }
    private void DeactivateAreaEffector()
    {
        areaEffector.enabled = false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawLine(new Vector3(-12, minY), new Vector3(12, minY));
        Gizmos.DrawLine(new Vector3(-12, maxY), new Vector3(12, maxY));
    }
}
