using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Events : MonoBehaviour
{
    static public Events instance;

    void Awake()
    {
        instance = this;
        print("A");
    }

    public event Action<Vector3> onFloorClicked;
    public void FloorClicked(Vector3 pos){
        if(onFloorClicked != null){
            onFloorClicked(pos);
        }
    }

}
