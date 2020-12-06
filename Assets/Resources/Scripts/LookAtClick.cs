using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtClick : MonoBehaviour
{
 
    [SerializeField] private float rotationSpeed;

    //values for internal use
    private Vector3 targetPos;
    private Quaternion lookRotation;
    private Vector3 direction;
     

    void Start(){
        Events.instance.onFloorClicked += LookAt;
    }
    void OnDestroy(){
        Events.instance.onFloorClicked -= LookAt;    
    }

    void LookAt(Vector3 pos){
        targetPos = pos;
        targetPos.y = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        //find the vector pointing from our position to the target
        direction = (targetPos - transform.position).normalized;

        if(direction != Vector3.zero){
            //create the rotation we need to be in to look at the target
            lookRotation = Quaternion.LookRotation(direction);

            //rotate us over time according to speed until we are in the required rotation
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
        }
    }
}
