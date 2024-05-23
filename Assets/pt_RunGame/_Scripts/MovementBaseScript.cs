using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;
using UnityEngine.Rendering;

public class MovementBaseScript : MonoBehaviour
{
    [SerializeField]
    PathCreator pathCreator;

    [SerializeField]
    PlayerScriptPT_Run player;

    [SerializeField]
    GameObject helpUI;

    Vector3 endPos;

    float moveDistance;

    // Start is called before the first frame update
    void Start()
    {
        endPos = pathCreator.path.GetPoint(pathCreator.path.NumPoints -1);
    }

    // Update is called once per frame
    void Update()
    {
        GetMouseButton();
        HelpUIPlay();
    }

    void GetMouseButton()//タップ中は走る
    {
        if(Input.GetMouseButton(0) && GameManagerScript.status == GameManagerScript.GAME_STATUS.Play)
        {
            moveDistance += player.speed * Time.deltaTime;
            transform.position = pathCreator.path.GetPointAtDistance(moveDistance,EndOfPathInstruction.Stop);
            transform.rotation = pathCreator.path.GetRotationAtDistance(moveDistance, EndOfPathInstruction.Stop);

            player.isRunning = true;
            helpUI.SetActive(false);
        }
        else if (Input.GetMouseButtonUp(0))
        {
            player.isRunning = false;
            helpUI.SetActive(true);
        }
    }

    void HelpUIPlay()//プレイ中以外は無効に
    {
        if(GameManagerScript.status != GameManagerScript.GAME_STATUS.Play)
        {
            helpUI.SetActive(false);
            return;
        }
    }
}
