using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class parallaxBackGround : MonoBehaviour
{
    private GameObject cam;
    // Start is called before the first frame update
    [SerializeField] private float parallaxEffect;

    private float xPositon;
    void Start()
    {
        cam = GameObject.Find("Main Camera");

        xPositon = transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        float distanceToMove = cam.transform.position.x * parallaxEffect;
        transform.position = new Vector3(xPositon+ distanceToMove, transform.position.y);
    }
}
