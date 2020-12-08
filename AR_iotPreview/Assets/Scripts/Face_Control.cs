using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Face_Control : MonoBehaviour
{
    Camera camera;

    // Start is called before the first frame update
    void Start()
    {
        camera = Camera.main;
        transform.GetChild(0).transform.rotation = Quaternion.Euler(0, 180, 0);
        transform.LookAt(camera.transform);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
