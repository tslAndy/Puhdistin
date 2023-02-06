using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HarpoonCannon : MonoBehaviour
{
    private void Update()
    {
        if(Time.timeScale != 0)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            float angle = Mathf.Atan2(mousePosition.y - transform.position.y, mousePosition.x - transform.position.x) * Mathf.Rad2Deg;
            angle = Mathf.Clamp(angle, -60, 30);
            transform.rotation = Quaternion.Euler(Vector3.forward * angle);
        }
    }
}
