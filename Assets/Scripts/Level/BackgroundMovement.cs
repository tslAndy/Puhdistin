using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BackgroundMovement : MonoBehaviour
{
    [Serializable]
    public class BackgroundLayer
    {
        public GameObject mainImage;
        public float speed;

        [NonSerialized]
        public GameObject leftImage, rightImage;

        [NonSerialized]
        public float width;
    }

    [SerializeField]
    private BackgroundLayer[] backgroundLayers;

    private BoxCollider2D CameraCollider;

    private float borderX;

    private void Start()
    {
        Application.targetFrameRate = 30;
        borderX = transform.InverseTransformPoint(GetComponent<BoxCollider2D>().bounds.max).x;

        foreach (BackgroundLayer backgroundLayer in backgroundLayers)
        {
            float width = backgroundLayer.width = backgroundLayer.mainImage.GetComponent<SpriteRenderer>().bounds.size.x;

            GameObject leftImage = Instantiate(backgroundLayer.mainImage, backgroundLayer.mainImage.transform);
            leftImage.transform.SetParent(transform);
            leftImage.transform.position += Vector3.left * width;

            backgroundLayer.leftImage = leftImage;

            GameObject rightImage = Instantiate(backgroundLayer.mainImage, backgroundLayer.mainImage.transform);
            rightImage.transform.SetParent(transform);
            rightImage.transform.position += Vector3.right * width;

            backgroundLayer.rightImage = rightImage;
        }
    }

    private void Update()
    {
        foreach (BackgroundLayer backgroundLayer in backgroundLayers)
        {
            Vector3 delta = Vector3.right * backgroundLayer.speed * Time.deltaTime;
            backgroundLayer.mainImage.transform.position += delta;
            backgroundLayer.leftImage.transform.position += delta;
            backgroundLayer.rightImage.transform.position += delta;


            float mainImageLeftX = backgroundLayer.mainImage.transform.localPosition.x - backgroundLayer.width / 2 - 2f;
            float leftImageLeftX = backgroundLayer.leftImage.transform.localPosition.x - backgroundLayer.width / 2 - 2f;
            float rightImageLeftX = backgroundLayer.rightImage.transform.localPosition.x - backgroundLayer.width / 2 - 2f;

            if (mainImageLeftX > borderX) backgroundLayer.mainImage.transform.position += Vector3.left * backgroundLayer.width * 3;
            else if (leftImageLeftX > borderX) backgroundLayer.leftImage.transform.position += Vector3.left * backgroundLayer.width * 3;
            else if (rightImageLeftX > borderX) backgroundLayer.rightImage.transform.position += Vector3.left * backgroundLayer.width * 3;
        }
    }
}
