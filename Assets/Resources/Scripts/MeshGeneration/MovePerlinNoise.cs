using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePerlinNoise : MonoBehaviour
{
    [SerializeField] private float speed;

    private Vector3 targetPos;

    void Start(){
        targetPos = transform.position - targetPos;
        
        Events.instance.onFloorClicked += SetTarget;
    }
    void OnDestroy(){
        Events.instance.onFloorClicked -= SetTarget;    
    }

    void SetTarget(Vector3 pos){
        targetPos = pos;
        targetPos.y = transform.position.y;
        targetPos = transform.position - targetPos;
    }

    // Update is called once per frame
    void Update()
    {
        // Move our position a step closer to the target.
        float step =  speed * Time.deltaTime; // calculate distance to move
        if ((transform.position - targetPos).magnitude < .1){
            Events.instance.StoppedMoving();
        }

        transform.position = Vector3.MoveTowards(transform.position, targetPos, step);

    }
}
