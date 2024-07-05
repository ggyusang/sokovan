using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalCube : MonoBehaviour
{
    public bool overlapped;
    public Material oldMaterial;
    public Material gpMaterial;
   
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("GoalPoint"))
        {

            overlapped = true;
            this.GetComponent<MeshRenderer>().material = gpMaterial;
        }
            
    }
    private void OnTriggerExit(Collider other)
    {
        overlapped = false;
        this.GetComponent<MeshRenderer>().material = oldMaterial;
    }
    // Start is called before the first frame update
    void Start()
    {
    
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
