using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletFire : MonoBehaviour
{
    public GameObject bullet;
    public float Bulletspeed = 2500;
    public int BulletNumber = 2;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0) && Input.GetMouseButton(1) && ShotGameManager.GameOnOff == true)// && BulletNumber > 0
        {
            GameObject Bullet = Instantiate(bullet,transform.position,Quaternion.Euler(transform.parent.eulerAngles.x,transform.parent.eulerAngles.y,0));
            Rigidbody bulletRb = Bullet.GetComponent<Rigidbody>();
            bulletRb.AddForce(transform.forward * Bulletspeed);
            Destroy(Bullet,2.0f);
            //BulletNumber -= 1;
        }
        //if(Input.GetKeyDown(KeyCode.R))
        //{
        //    BulletNumber = 2;
        //    Debug.Log("ÉäÉçÅ[Éh");
        //}
    }

}
