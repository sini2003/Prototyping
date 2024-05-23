using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombEffectScript : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerScriptPT_Run>().TakeDamage();
        }
    }
}