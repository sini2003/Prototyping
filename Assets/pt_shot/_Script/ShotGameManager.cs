using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ShotGameManager : MonoBehaviour
{
    public GameObject target;//�I
    public static int ShotScore = 0;//�Q�[���X�R�A
    float CountTime = 60;//Play����
    public static bool GameOnOff = false;
    public bool DevelopedOnOff = false;//���������p
    public float SetTime = 3;
    GameObject newTarget1;
    Vector3 Target2Pos;
    Vector3 TarVec;
    Vector3 RotationVec = new Vector3(0,180,0);

    [SerializeField]
    Text TimeCountText,ScoreText,EndScoreText;
    [SerializeField]
    GameObject StartButton, GameENDTexxt;

    // Start is called before the first frame update
    void Start()
    {
        //���� 0�b��Ăяo���A�ȍ~SetTime�b�����s
        InvokeRepeating("CreateTarget1", 1f, SetTime);
        InvokeRepeating("CreateTarget2", 1f, SetTime);
        if(DevelopedOnOff == true)//���������p
        {
            GameOnOff = true;
        }
    }
    void CreateTarget1()
    {
        if (GameOnOff == true)
        {
            //����
            newTarget1 = Instantiate(target, new Vector3(Random.Range(-4 - (ShotScore * 1 / 50), 5 + (ShotScore * 1 / 50)), Random.Range(6, 10), 15), Quaternion.Euler(RotationVec));
            //�폜
            Destroy(newTarget1, SetTime);
        }
    }
    void CreateTarget2()
    {
        if (GameOnOff == true)
        {
            int RandomPos = Random.Range(-4 - (ShotScore * 1 / 20), 5 + (ShotScore * 1 / 20));
            Target2Pos = new Vector3(RandomPos, Random.Range(5, 11), 15);
            TarVec = newTarget1.transform.position;
            if (TarVec == Target2Pos)
            {
                Debug.LogFormat("������A��蒼���I");
                RandomPos = Random.Range(-4 - (ShotScore * 1 / 20), 5 + (ShotScore * 1 / 20));
                Target2Pos = new Vector3(RandomPos, Random.Range(6, 10), 15);
                //����
                GameObject newTarget2 = Instantiate(target, Target2Pos, Quaternion.Euler(RotationVec));
                //�폜
                Destroy(newTarget2, SetTime);
            }
            else
            {
                //����
                GameObject newTarget2 = Instantiate(target, Target2Pos, Quaternion.Euler(RotationVec));
                //�폜
                Destroy(newTarget2, SetTime);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.Space)&&GameOnOff==false)
        {
            GameStart();
        }
        Score();
        TimeCount();
    }
    void TimeCount()//�Q�[�����ԃJ�E���g
    {
        if (CountTime >= 0f�@&& GameOnOff == true)
        {
            CountTime -= Time.deltaTime;
            TimeCountText.text = CountTime.ToString("F2");
        }
        else if(CountTime <= 0f)
        {
            GameOnOff = false;
            GameEND();
        }
    }
    void Score()
    {
        ScoreText.text = "Score " + ShotScore;
    }
    void GameStart()
    {
        StartButton.SetActive(false);
        GameOnOff = true;
    }
    void GameEND()
    {
        GameENDTexxt.SetActive(true);
        EndScoreText.text = "SCORE"+ShotScore;
    }
    public void GameRetry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        GameOnOff = false;
        ShotScore = 0;
    }
}