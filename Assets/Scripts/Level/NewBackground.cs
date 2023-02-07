using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class NewBackground : MonoBehaviour
{
    [SerializeField]
    private BackLayer[] backLayers;

    public BackLayer[] BackLayers { get { return backLayers; } }



    void Start()
    {
        foreach (BackLayer backLayer in backLayers)
        {
            backLayer.images = new GameObject[3]; 

            GameObject layer = new GameObject(backLayer.image.name + "_layers");
            layer.transform.SetParent(transform);

            backLayer.width = backLayer.image.GetComponent<SpriteRenderer>().bounds.size.x - 0.015f;
            for (int i = 0; i < 3; i++)
            {
                GameObject image = Instantiate(backLayer.image, transform);
                image.transform.SetParent(layer.transform);
                image.transform.position += Vector3.right * backLayer.width * i;
                backLayer.images[i] = image;
            }

            Destroy(backLayer.image);
        }
    }

    void Update()
    {
        foreach (BackLayer backLayer in backLayers)
        {
            float dx = backLayer.speed * Time.deltaTime;
            foreach (GameObject image in backLayer.images)
            {
                image.transform.position += Vector3.left * dx;

         
                if (image.transform.position.x + backLayer.width / 2 < 
                    Camera.main.ViewportToWorldPoint(Vector3.zero).x)
                {
                    image.transform.position += Vector3.right * backLayer.width * 3;
                }
            }
        }
    }
}

[Serializable]
public class BackLayer
{
    public GameObject image;
    public float speed;

    [NonSerialized]
    public GameObject[] images;

    [NonSerialized]
    public float width;
}