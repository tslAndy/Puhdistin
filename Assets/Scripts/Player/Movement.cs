using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField]
    private float minX, maxX;

    [SerializeField]
    private float speed;

    [SerializeField]
    private float dSin;

    private float valueForSin = 0f;
    private float defaultY;

    private bool canMove = true;

    public bool CanMove { get; set; }

    private void Start()
    {
        defaultY = transform.position.y;
    }
    private void Update()
    {
        float xDirection = Input.GetAxisRaw("Horizontal");
        float dx = speed * xDirection * Time.deltaTime;
        if (CanMove && (minX < transform.position.x + dx && transform.position.x + dx < maxX))
            transform.position += Vector3.right * dx;

        if(CanMove)
            valueForSin += dSin * Time.deltaTime;

        if (float.MaxValue - valueForSin < 1000)
            valueForSin = 0;

        float dy = Mathf.Sin(valueForSin) * 0.3f;
        transform.position = new Vector3(transform.position.x, defaultY + dy, 0) ;

    }


}
