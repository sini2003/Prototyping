using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalScript : MonoBehaviour
{
    Vector3 centerPos;

    // Start is called before the first frame update
    void Start()
    {
        centerPos = new Vector3(transform.position.x,0,transform.position.z);
    }

    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            other.GetComponent<PlayerScriptPT_Run>().Clear(centerPos);
        }
    }
}
