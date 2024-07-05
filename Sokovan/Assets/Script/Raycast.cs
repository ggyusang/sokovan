using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Raycast : MonoBehaviour
{
   /* // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F))
        {
            Debug.DrawRay(transform.position, transform.forward*  15f, Color.white, 1f);
           // Debug.DrawRay(transform.position, transform.forward * 1.5f, Color.blue, 0.5f);
            if (Physics.Raycast(transform.position,transform.forward,out shortHit, 1))
            {
                if(shortHit.collider.CompareTag("Wall"))
                {
                    Debug.Log("바로 앞 벽 움직일수 없음");
                }
             
                else
                {
                    multiHit = Physics.RaycastAll(transform.position, transform.forward, MaxDistance);
                    for (int i = 0; i<multiHit.Length; i++)
                    {
                        RaycastHit hit = multiHit[i];
                        if(hit.collider.CompareTag("Wall")|| hit.collider.CompareTag("GoalCube"))
                        {
                            
                            obstacle += 1;
                        }
                        if (obstacle >= 2)
                        {
                            Debug.Log(obstacle);
                            Debug.Log("두칸내에 장애물이 두개 있어서 움직일수 없음");

                        }
                    }
                    if(shortHit.collider.CompareTag("GoalCube"))
                    {
                        shortHit.collider.transform.position = new Vector3(0, 10, 0);
                    }
                    obstacle = 0;
                    
                    
                }
                    
              

            }
          
    
        }
    }*/
}
