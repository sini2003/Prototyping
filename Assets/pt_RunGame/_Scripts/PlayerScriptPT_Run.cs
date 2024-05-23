using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScriptPT_Run : MonoBehaviour
{
    Animator anim;

    public bool isRunning;

    public float sensitivity = 1f;
    const float LOAD_WIDTH = 6f;
    const float MOVE_MAX = 2.5f;
    Vector3 previousPos, currentPos;

    Vector3 dest;//���̖ړI�n�B�N���A���Ɏg�p

    public float speed = 6f;

    void Start()
    {
        anim = GetComponent<Animator>();

        isRunning = false;
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetBool("IsRunning", isRunning);

        //�N���A���̏���
        if (GameManagerScript.status == GameManagerScript.GAME_STATUS.Clear)
        {
            //�ړI�n�̕���������
            transform.LookAt(dest);

            //�ړI�n�̕����Ɉړ�������
            Vector3 dir = (dest - transform.position).normalized;
            transform.position += dir * speed * Time.deltaTime;

            //�ړI�n�ɏ\���n��������A�ŏI���o
            if ((dest - transform.position).magnitude < 0.5f)
            {
                transform.position = dest;
                transform.rotation = Quaternion.Euler(0, 180, 0);
                anim.SetBool("IsRunning", false);
                anim.SetTrigger("Clear");

                enabled = false;
            }
            return;
        }

        //�v���C�ȊO�Ȃ疳���ɂ���
        if (GameManagerScript.status != GameManagerScript.GAME_STATUS.Play)
        {
            anim.SetBool("IsRunning", false);
            return;
        }

        if (Input.GetMouseButtonDown(0))
        {
            previousPos = Input.mousePosition;
        }
        if (Input.GetMouseButton(0))
        {
            //�X���C�v�ɂ��ړ��������擾
            currentPos = Input.mousePosition;
            float diffdistanc = (currentPos.x - previousPos.x) / Screen.width * LOAD_WIDTH;
            diffdistanc *= sensitivity;

            //���̃��[�J��X���W��ݒ�
            float newX = Mathf.Clamp(transform.localPosition.x + diffdistanc, -MOVE_MAX, MOVE_MAX);
            transform.localPosition = new Vector3(newX, 0, 0);

            //�^�b�v�ʒu���X�V
            previousPos = currentPos;
        }
    }
    public void Clear(Vector3 pos)
    {
        GameManagerScript.status = GameManagerScript.GAME_STATUS.Clear;
        dest = pos;
    }
    public void TakeDamage()
    {
        anim.SetTrigger("Damaged");
        GameManagerScript.status = GameManagerScript.GAME_STATUS.GameOver;
    }
}
