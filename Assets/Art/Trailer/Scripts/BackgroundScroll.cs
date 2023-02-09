using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroll : MonoBehaviour
{
    [SerializeField]
    private float speed;

    private void Update()
    {
        transform.position += Vector3.down * speed * Time.deltaTime;
    }
}
