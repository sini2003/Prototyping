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

    Vector3 dest;//次の目的地。クリア時に使用

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

        //クリア時の処理
        if (GameManagerScript.status == GameManagerScript.GAME_STATUS.Clear)
        {
            //目的地の方向を向く
            transform.LookAt(dest);

            //目的地の方向に移動させる
            Vector3 dir = (dest - transform.position).normalized;
            transform.position += dir * speed * Time.deltaTime;

            //目的地に十分地数いたら、最終演出
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

        //プレイ以外なら無効にする
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
            //スワイプによる移動距離を取得
            currentPos = Input.mousePosition;
            float diffdistanc = (currentPos.x - previousPos.x) / Screen.width * LOAD_WIDTH;
            diffdistanc *= sensitivity;

            //次のローカルX座標を設定
            float newX = Mathf.Clamp(transform.localPosition.x + diffdistanc, -MOVE_MAX, MOVE_MAX);
            transform.localPosition = new Vector3(newX, 0, 0);

            //タップ位置を更新
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
