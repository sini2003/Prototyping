using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject Hit_ef;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Target"))
        {
            Destroy(collision.gameObject);
            Destroy(this.gameObject);
            GameObject NewHit_ef = Instantiate(Hit_ef,this.transform.position,Quaternion.identity);
            Destroy(NewHit_ef, 0.5f);
            Debug.Log("Hit");
            ShotGameManager.ShotScore++;
            Debug.Log("åÇîjêî:" + ShotGameManager.ShotScore);
        }
    }
}
