using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int count = 0;
    public  Scene stage ;
    private int stageNumber;
    private string nextStage;
    public GameObject gameOverUI;
    public GameObject gameOverUI2;
    public bool isGameOver1;
    public List<GoalCube> goalcubes;

    public static GameManager instance = null;
    // Start is called before the first frame update
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
            //씬 이동이 되었을때  GameManager 가 존재하면 
            Destroy(this.gameObject);
        }
    }
    void Start()
    {

        stageNumber = 1;
    }

    // Update is called once per frame
    void Update()
    {
        Scene scene = SceneManager.GetActiveScene();
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene(scene.name);
        }

       
        if (isGameOver1 == true)
        {
            ScoreManager.instance.myScore();
            Debug.Log("game over");
            gameOverUI.SetActive(true);
            gameOverUI2.SetActive(true);
            if (Input.GetKeyDown(KeyCode.F))
            {
                stageNumber += 1;
                nextStage = ("Stage"+stageNumber);
                SceneManager.LoadScene(nextStage);
                isGameOver1 = false;
            }
           
        }


        count = 0;
    
        for (int i=0;  i<goalcubes.Count; i++)
        {
            if(goalcubes[i].overlapped==true)
            {
                count++;
            }
        }
        if(count >=goalcubes.Count)
        {
           
            isGameOver1 = true;
            
         
        }

    
     }   
    
}
