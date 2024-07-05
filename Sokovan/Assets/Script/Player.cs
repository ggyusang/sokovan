using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Transform playerTransform;
    private Transform goalCubeTransform;
    //RayCast
    int obstacle = 0;
    RaycastHit[] multiHit;
    RaycastHit shortHit;
    float MaxDistance = 2f;
    // 이동

    [SerializeField] float speed = 10f;
    Vector3 dir = new Vector3();
    Vector3 destinationPos = new Vector3();
    Vector3 destinationPosgoal = new Vector3();
    public Rigidbody playerRigidbody;

    // 회전
    [SerializeField] float spinSpeed = 270;
    Vector3 rotDir = new Vector3();
    Quaternion destinationRot = new Quaternion();

    // 회전할때 돌려놓고 그값을 저장시킬 가짜 큐브
   // [SerializeField] Transform fakeCube = null;
    private Transform realCube = null;

    bool canMove = true;
   
    public GameManager gameManager;

    void Start()
    {
        
    }


    void Update()
    {
        
        float inputX = Input.GetAxisRaw("Horizontal");
        float inputZ = Input.GetAxisRaw("Vertical");

        if (gameManager.isGameOver1 == true)
            {
                return;
            }

            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D))
            {
          
            Debug.DrawRay(transform.position, new Vector3(inputX, 0, inputZ) * 2f, Color.white, 1f); ;
            // Debug.DrawRay(transform.position, transform.forward * 1.5f, Color.blue, 0.5f);
            if (Physics.Raycast(transform.position,new Vector3(inputX, 0, inputZ), out shortHit, 1))
            {
                if (shortHit.collider.CompareTag("Wall"))
                {
                    Debug.Log("바로앞에 벽이있음");
                    return;
                }
                
                else
                {
                    obstacle = 0;
                    multiHit = Physics.RaycastAll(transform.position, new Vector3(inputX,0,inputZ), MaxDistance);
                    for (int i = 0; i < multiHit.Length; i++)
                    {
                        RaycastHit hit = multiHit[i];
                        if (hit.collider.CompareTag("Wall") || hit.collider.CompareTag("GoalCube"))
                        {

                            obstacle += 1;
                        }
                        if (obstacle >= 2)
                        {
                            Debug.Log("두칸내에 장애물이 두개 이상 있음");
                            Debug.Log(obstacle);


                            return;
                        }
                      


                    }
                    if (shortHit.collider.CompareTag("GoalPoint"))
                    {
                        if (canMove == true)
                        {
                            StartAction();
                        }
                    }    

                        if (shortHit.collider.CompareTag("GoalCube"))
                    {
                        if (canMove==true)
                        {
                            Debug.Log("바로앞에 goalcube가 있음");
                            goalCubeTransform = shortHit.collider.transform;
                            Debug.Log(goalCubeTransform.position);
                            realCube = shortHit.collider.transform;
                            StartAction2();
                        }
                      
                    }
                    


                }
               
            }
            else
            {
                if (canMove)
                {

                    Debug.Log("앞에 아무것도 존재하지 않음");
                   
                    StartAction();
                }
            }



            /*
            if(Input.GetKeyUp(KeyCode.W))
            {
                transform.position = Vector3.Lerp(transform.position,transform.position+Vector3.forward, 1f);
            }
            /*
            float inputX = Input.GetAxis("Horizontal");
            float inputZ = Input.GetAxis("Vertical");





            float fallSpeed = playerRigidbody.velocity.y;

            Vector3 velocity = new Vector3(inputX, 0, inputZ);

            velocity *= speed;

            velocity.y = fallSpeed;
            playerRigidbody.velocity = velocity;*/
           

        }
       
    }

    private void StartAction2()
    {
        ScoreManager.instance.AddScore();
        //이동
        /*    dir.Set(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
            destinationPos = transformparam.position + new Vector3(dir.x, 0, dir.z);
            */
        //회전
        rotDir = new Vector3(dir.z, 0f, -dir.x);
       // fakeCube.RotateAround(shortHit.transform.position, rotDir, spinSpeed);
      //  destinationRot = fakeCube.rotation;

        //코루틴시작
        StartCoroutine(Move_goalCo(goalCubeTransform));
        StartCoroutine(MoveCo(playerTransform));
     //   StartCoroutine(SpinCo());

    }
    private void StartAction()
    {
        ScoreManager.instance.AddScore();

        //이동
        dir.Set(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        destinationPos = transform.position + new Vector3(dir.x, 0, dir.z);
     
        //회전
    
        //코루틴시작
        StartCoroutine(MoveCo(playerTransform));
       // StartCoroutine(SpinCo());

    }
    IEnumerator Move_goalCo(Transform transform_param)
    {
        dir.Set(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        destinationPosgoal = transform_param.position + new Vector3(dir.x, 0, dir.z);
        Debug.Log(destinationPos);
        canMove = false;
        while (Vector3.SqrMagnitude(transform_param.position - destinationPosgoal) >= 0.0001f)
        {


            transform_param.position = Vector3.MoveTowards(transform_param.position, destinationPosgoal, speed * Time.deltaTime);
          
            yield return null;
        }
        Debug.Log("while문 안에서 transfromparamposition" + transform_param.position);
        transform_param.position = destinationPosgoal;
        canMove = true;
    }
    IEnumerator MoveCo(Transform transform_param)
    {
        dir.Set(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        destinationPos = transform_param.position + new Vector3(dir.x, 0, dir.z);

        canMove = false;
        while (Vector3.SqrMagnitude(transform_param.position - destinationPos) >= 0.0001f)
        {

            transform_param.position = Vector3.MoveTowards(transform_param.position, destinationPos, speed * Time.deltaTime);
            yield return null;
        }
        transform_param.position = destinationPos;
        canMove = true;
    }
    IEnumerator SpinCo()
    {
        while(Quaternion.Angle(realCube.rotation,destinationRot)>0.5f)
        {
           realCube.rotation = Quaternion.RotateTowards(realCube.rotation, destinationRot, spinSpeed * Time.deltaTime);
            yield return null;
        }
        realCube.rotation = destinationRot;
    }


}
 
        
        