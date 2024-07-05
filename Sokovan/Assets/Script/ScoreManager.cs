using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class ScoreManager : MonoBehaviour
{
    public int currentMove;
    public int highMinMove;

    public Text txtCurrentMove;

    // 보안을 위해선 private 으로 만들어야함
    public static ScoreManager instance = null;

    Scene scene;
   public string currentStage;
    private void Awake()
    {
       
        if (null == instance)
        {
            instance = this;

            //씬 전환이 되더라도 파괴되지 않게 함
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
          //씬 이동이 되었을때  ScoreManager 가 존재하면 
            Destroy(this.gameObject);
        }
    }
    void Start()
    {
        LoadHighMinMove(); //최고점 불러오기.
   
    }

    public void myScore()
    {
       if(currentMove < highMinMove)
        {
            highMinMove = currentMove;
            SaveHighMinMove();
        }
    }
   public void SaveHighMinMove()
    {
        
        PlayerPrefs.SetInt("HighMinMove"+currentStage, highMinMove);
    }
        private void LoadHighMinMove()

    {
        highMinMove = PlayerPrefs.GetInt("HighMinMove"+currentStage);//최고점수 불러오기 초기
    }

    public void AddScore()
    {
        currentMove += 1;
        
    }

    // Update is called once per frame
    void Update()
    {
        currentStage = scene.name;
        txtCurrentMove.text = string.Format("Current Move : {0}\nBest Score : {1}", currentMove, PlayerPrefs.GetInt("HighMinMove" + currentStage));
        scene = SceneManager.GetActiveScene();
        Debug.Log(highMinMove);
        Debug.Log(currentMove);
        Debug.Log(PlayerPrefs.GetInt("highMinMove" + currentStage));
    }
}
