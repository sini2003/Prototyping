using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseZoom : MonoBehaviour
{
    Camera cam;

    void Start()
    {
        cam = Camera.main;
            
    }
    // Start is called before the first frame update
    void Update()
    {
        cam.fieldOfView = 25;
    }
}
