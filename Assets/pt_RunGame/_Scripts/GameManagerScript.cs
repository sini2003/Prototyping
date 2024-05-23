using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManagerScript : MonoBehaviour
{
    public enum GAME_STATUS { Play,Clear,Pause,GameOver};
    //Play(�v���C��),Clear(�N���A��),Pause(�ꎞ��~),GameOver(�Q�[���I�[�o�[��)
    public static GAME_STATUS status;

    public static int tempCoinNum;

    [SerializeField]
    Text coinNumText,resultCoinText,levelNumText;

    [SerializeField]
    GameObject clearUI,gameOverUI, adminStopeUI;

    int stageCoinNum;

    const string STAGE_NAME_PREFIX = "Stage";
    const int MAX_STAGE_NUM = 3;

    int levelNum;//���݂̐i�s��
    int stageNum;//�ǂݍ��ރX�e�[�W�ԍ�

    // Start is called before the first frame update
    void Start()
    {
        //�X�e�[�W�ԍ����[�h
        stageNum = PlayerPrefs.GetInt("stageNum", 1);

        //�����̃V�[���ł͂Ȃ��ꍇ�A���[�h���Ȃ���
        if(!GetLoadSceneName().Equals(SceneManager.GetActiveScene().name))
        {
            LoadScene();
            return;
        }

        //���x���ԍ������[�h
        levelNum = PlayerPrefs.GetInt("levelNum", 1);
        levelNumText.text = "Level" + levelNum;

        //�X�e�[�W���̃R�C���̖������擾
        stageCoinNum = GameObject.FindGameObjectsWithTag("Coin").Length;

        //����܂ł̊l���R�C���������[�h(�����0)
        tempCoinNum = PlayerPrefs.GetInt("coinNum", 0);

        //�X�e�[�^�X��play��
        status = GAME_STATUS.Play;
    }

    // Update is called once per frame
    void Update()
    {

        if (status == GAME_STATUS.Clear)
        {
            //���݂̃X�e�[�W�Ŋl�������R�C���̖���
            int getCoinNum = tempCoinNum - PlayerPrefs.GetInt("coinNum", 0);

            resultCoinText.text = getCoinNum.ToString().PadLeft(3) + "/" + stageCoinNum;
            clearUI.SetActive(true);

            //�R�C����ۑ�
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

        if (Input.GetKey(KeyCode.F3))//unity�㐧��҃��Z�b�g�p
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


    public void ResetGame()//���Z�b�g�p�{�^��
    {
        PlayerPrefs.SetInt("coinNum", 0);
        PlayerPrefs.SetInt("levelNum", 1);
        PlayerPrefs.SetInt("stageNum", 1);
        SceneManager.LoadScene("Stage1");
    }
}
