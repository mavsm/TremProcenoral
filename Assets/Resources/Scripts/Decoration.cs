using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Decoration : MonoBehaviour
{
    [SerializeField] float zStartLowering = 5.0f;
    [SerializeField] float zLowerStartLowering = -5.0f;

    private float initialY;

    void Awake(){
        initialY = transform.position.y;
    }
    
    // Update is called once per frame
    void Update()
    {
        if(transform.position.z > zStartLowering){
            Vector3 newPos = transform.position;
            newPos.y = initialY - (transform.position.z - zStartLowering);
            transform.position = newPos;
        }
        else if(transform.position.z < zLowerStartLowering){
            Vector3 newPos = transform.position;
            newPos.y = initialY - (zLowerStartLowering - transform.position.z);
            transform.position = newPos;
        }
    }
}
