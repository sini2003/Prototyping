using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombScript : MonoBehaviour
{
    Animator anim;

    [SerializeField]
    Transform endPoint;

    Vector3 startPos;   //�J�n�ʒu
    Vector3 endPos;     //�I�[�ʒu
    Vector3 destPos;    //���̖ړI�n

    float speed = 1f;
    float rotatespeed = 180f;
    float rotateNum;
    
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();

        startPos = transform.position;
        endPos = endPoint.position;
        destPos = endPos;

        //�ړ����x�������_����
        speed = Random.Range(1.0f, 3.0f);
        //�J�n�ʒu�������_����
        transform.position = Vector3.Lerp(startPos, endPos, Random.Range(0.0f, 1.0f));
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManagerScript.status != GameManagerScript.GAME_STATUS.Play)
        {
            return;
        }
        //�[�ɓ��B�������̕����]��
        if ((destPos - transform.position).magnitude < 0.1f)
        {
            //��]�r���̏ꍇ
            if(rotateNum < 180)
            {
                anim.SetBool("walk", false);
                transform.position = destPos;

                float addNum = rotatespeed * Time.deltaTime;
                rotateNum += addNum;
                transform.Rotate(0, addNum, 0);
                return;
            }
            //��]���؂����ꍇ
            //���̖ړI�n�̐ݒ�Ɖ�]�ʂ̃��Z�b�g
            else
            {
                destPos = destPos == startPos ? endPos : startPos;
                rotateNum = 0;
            }
        }

        //���̖ړI�n�Ɍ����Ĉړ�����
        anim.SetBool("walk", true);
        transform.LookAt(destPos);
        transform.position += transform.forward * speed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(GameManagerScript.status != GameManagerScript.GAME_STATUS.Play)
        {
            return;
        }

        if(other.CompareTag("Player"))
        {
            transform.LookAt(other.gameObject.transform);
            other.gameObject.transform.LookAt(transform);
            anim.SetBool("walk", false);
            anim.SetTrigger("attack01");

            GameManagerScript.status = GameManagerScript.GAME_STATUS.Pause;
        }
    }
}
