using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HarpoonThrowing : MonoBehaviour
{
    [SerializeField]
    private float speed;

    [SerializeField]
    private GameObject harpoon;

    private Rigidbody2D harpoonRb;

    private void Start()
    {
        harpoonRb = harpoon.GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire1") && harpoonRb.bodyType == RigidbodyType2D.Static)
        {
            Vector3 clickPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 direction = (clickPosition - harpoon.transform.position).normalized;
            harpoonRb.bodyType = RigidbodyType2D.Dynamic;
            harpoonRb.velocity = direction * speed * Mathf.Sign(transform.localScale.x);
            Debug.Log(direction * speed * transform.localScale.x);
        }
    }
}
