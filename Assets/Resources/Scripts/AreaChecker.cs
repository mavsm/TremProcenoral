using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaChecker : MonoBehaviour
{ 
    [SerializeField] private LayerMask worldLayer;

    private void OnTriggerExit(Collider other)
    {
        if (((1 << other.gameObject.layer) & worldLayer) != 0)
        {
            Destroy(other.gameObject);
        }
    }
}
