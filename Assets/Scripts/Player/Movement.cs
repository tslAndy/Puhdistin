using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField]
    private float speed;

    [SerializeField]
    private float dSin;

    private float valueForSin = 0f;
    private float defaultY;

    private void Start()
    {
        defaultY = transform.position.y;
    }
    private void Update()
    {
        float xDirection = Input.GetAxisRaw("Horizontal");
        transform.position += Vector3.right * speed * xDirection * Time.deltaTime;

        valueForSin += dSin * Time.deltaTime;

        if (float.MaxValue - valueForSin < 1000)
            valueForSin = 0;

        float dy = Mathf.Sin(valueForSin) * 0.3f;
        transform.position = new Vector3(transform.position.x, defaultY + dy, 0) ;

    }


}
