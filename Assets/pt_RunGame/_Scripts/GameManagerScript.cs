using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManagerScript : MonoBehaviour
{
    public enum GAME_STATUS { Play,Clear,Pause,GameOver};
    //Play(プレイ中),Clear(クリア時),Pause(一時停止),GameOver(ゲームオーバー時)
    public static GAME_STATUS status;

    public static int tempCoinNum;

    [SerializeField]
    Text coinNumText,resultCoinText,levelNumText;

    [SerializeField]
    GameObject clearUI,gameOverUI, adminStopeUI;

    int stageCoinNum;

    const string STAGE_NAME_PREFIX = "Stage";
    const int MAX_STAGE_NUM = 3;

    int levelNum;//現在の進行数
    int stageNum;//読み込むステージ番号

    // Start is called before the first frame update
    void Start()
    {
        //ステージ番号ロード
        stageNum = PlayerPrefs.GetInt("stageNum", 1);

        //自分のシーンではない場合、ロードしなおす
        if(!GetLoadSceneName().Equals(SceneManager.GetActiveScene().name))
        {
            LoadScene();
            return;
        }

        //レベル番号をロード
        levelNum = PlayerPrefs.GetInt("levelNum", 1);
        levelNumText.text = "Level" + levelNum;

        //ステージ内のコインの枚数を取得
        stageCoinNum = GameObject.FindGameObjectsWithTag("Coin").Length;

        //これまでの獲得コイン数をロード(初回は0)
        tempCoinNum = PlayerPrefs.GetInt("coinNum", 0);

        //ステータスをplayに
        status = GAME_STATUS.Play;
    }

    // Update is called once per frame
    void Update()
    {

        if (status == GAME_STATUS.Clear)
        {
            //現在のステージで獲得したコインの枚数
            int getCoinNum = tempCoinNum - PlayerPrefs.GetInt("coinNum", 0);

            resultCoinText.text = getCoinNum.ToString().PadLeft(3) + "/" + stageCoinNum;
            clearUI.SetActive(true);

            //コインを保存
            PlayerPrefs.SetInt("coinNum", tempCoinNum);

            enabled = false;
        }
        else if (status == GAME_STATUS.GameOver)
        {
            Invoke("ShowGameOverUI", 3f);
            enabled = false;
            return;
        }

        coinNumText.text = tempCoinNum.ToString();

        if (Input.GetKey(KeyCode.F3))//unity上制作者リセット用
        {
            adminStopeUI.SetActive(true);
        }

    }

    public string GetLoadSceneName()
    {
        return STAGE_NAME_PREFIX + stageNum;
    }

    public void LoadScene()
    {
        SceneManager.LoadScene(GetLoadSceneName());
    }

    public void LoadCurrentScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void LoadNextScene()
    {
        PlayerPrefs.SetInt("levelNum", ++levelNum);

        stageNum = levelNum <= MAX_STAGE_NUM ? levelNum : Random.Range(1, MAX_STAGE_NUM + 1);
        PlayerPrefs.SetInt("stageNum", stageNum);

        LoadScene();
    }
    private void ShowGameOverUI()
    {
        gameOverUI.SetActive(true);
    }


    public void ResetGame()//リセット用ボタン
    {
        PlayerPrefs.SetInt("coinNum", 0);
        PlayerPrefs.SetInt("levelNum", 1);
        PlayerPrefs.SetInt("stageNum", 1);
        SceneManager.LoadScene("Stage1");
    }
}
