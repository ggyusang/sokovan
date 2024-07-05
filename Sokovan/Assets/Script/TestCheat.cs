using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCheat : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            GameManager.instance.isGameOver1 = true;
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            Debug.Log("스테이지1=" + PlayerPrefs.GetInt("Stage1") + "스테이지2=" + PlayerPrefs.GetInt("Stage2"));
            Debug.Log(ScoreManager.instance.currentStage);
            PlayerPrefs.SetInt("HighMinMove"+ScoreManager.instance.currentStage, 9999);
            ScoreManager.instance.highMinMove = 9999;
            ScoreManager.instance.SaveHighMinMove();
        }
    }
}