using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomizeHeight : MonoBehaviour
{
    [SerializeField] Vector2 heightRange;

    // Start is called before the first frame update
    void Start()
    {
        Vector3 newPos = transform.position;
        newPos.y = Random.Range(heightRange.x, heightRange.y);
        transform.position = newPos;
    }


}
