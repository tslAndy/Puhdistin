using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Background : MonoBehaviour
{
    [Serializable]
    public class Layer
    {
        [SerializeField]
        public GameObject image;

        public float speed;

        [NonSerialized]
        public List<GameObject> images;

        [NonSerialized]
        public float width;

        [NonSerialized]
        public int imagesAmount = 3;

        public void CalculateWidth() => width = image.GetComponent<SpriteRenderer>().bounds.size.x - 0.01f;
        public Layer() => images = new List<GameObject>();
    }

    [SerializeField]
    private Layer[] layers;

    public Layer[] Layers { get { return layers; } set { layers = value; } }

    public float this[int index]
    {
        get => Layers[index].speed;
        set => layers[index].speed = value;
    }

    private void Start()
    {
        foreach (Layer layer in layers)
        {
            layer.CalculateWidth();
            layer.images.Add(layer.image);
            for (int i = 1; i < layer.imagesAmount; i++)
            {
                GameObject newImage = Instantiate(layer.image, layer.image.transform);
                newImage.transform.SetParent(layer.image.transform.parent);
                newImage.transform.position += Vector3.right * layer.width * i;
                layer.images.Add(newImage);
            }
        }
    }

    private void Update()
    {
        foreach (Layer layer in layers)
        {
            foreach (GameObject image in layer.images)
            {
                image.transform.position += Vector3.left * layer.speed * Time.deltaTime;

                if (image.transform.position.x + layer.width / 2 < Camera.main.ViewportToWorldPoint(Vector3.zero).x)
                {
                    image.transform.position += Vector3.right * layer.width * (layer.images.Count);
                }
            }
        }
    }

}
