using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuckBackground : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Renderer bgRenderer;

    private void Update()
    {
        bgRenderer.material.mainTextureOffset += new Vector2(speed * Time.deltaTime, 0);
    }
}
