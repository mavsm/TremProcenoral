using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CyclingBoundary : MonoBehaviour
{
    [SerializeField] private Vector2 upperBounds;
    [SerializeField] private Vector2 lowerBounds;
    [SerializeField] private Vector2Event cycledEvent;


    // Update is called once per frame
    void Update()
    {
        Vector3 newPos = transform.position;
        Vector2 movementDiff = new Vector2();
    
        if(transform.position.x > upperBounds.x) {
            float diff = transform.position.x - upperBounds.x;
            newPos.x = lowerBounds.x + diff;
            movementDiff.x = 10;
        }
        else if(transform.position.x < lowerBounds.x) {
            float diff = lowerBounds.x - transform.position.x;
            newPos.x = upperBounds.x + diff;
            movementDiff.x = -10;
        }
        if(transform.position.z > upperBounds.y) {
            float diff = transform.position.z - upperBounds.y;
            newPos.z = lowerBounds.y + diff;
            movementDiff.y = 10;
        }
        else if(transform.position.z < lowerBounds.y) {
            float diff = lowerBounds.y - transform.position.z;
            newPos.z = upperBounds.y + diff;
            movementDiff.y = -10;
        }

        if(movementDiff != Vector2.zero){
            cycledEvent.Invoke(movementDiff);
        }
        transform.position = newPos;
    }
}

[System.Serializable]
public class Vector2Event : UnityEvent<Vector2>
{}