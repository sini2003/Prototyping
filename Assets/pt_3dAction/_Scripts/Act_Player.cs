using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Act_Player : MonoBehaviour
{
    Vector3 PlayerPos;

    // Start is called before the first frame update
    void Start()
    {
        PlayerPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W ))
        {
            transform.position += new Vector3(0, 0, 0.1f);
        }
        if (Input.GetKey(KeyCode.S ))
        {
            transform.position += new Vector3(0, 0, -0.1f);
        }
        if (Input.GetKey(KeyCode.A ))
        {
            transform.position += new Vector3(0.1f, 0, 0);
        }
        if (Input.GetKey(KeyCode.D ))
        {
            transform.position += new Vector3(-0.1f, 0, 0);
        }
        PlayerPos = transform.position;
    }
}
