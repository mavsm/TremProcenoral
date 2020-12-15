using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DentedPixel;

public class LightManager : MonoBehaviour
{
    [SerializeField] private float changeDuration = 0.5f;

    private Vector3 posToLookAt = new Vector3();
    private Light light;

    void Start(){
        light = GetComponent<Light>();
    }

    
    // Update is called once per frame
    void Update()
    {
        if(!LeanTween.isTweening(gameObject) && light.color != BiomeManager.instance.GetLightColorAtPos(posToLookAt)){
            Color toColor = BiomeManager.instance.GetLightColorAtPos(posToLookAt);
            LeanTween.value(gameObject, changeColor, light.color, toColor, changeDuration);
        }
    }

    private void changeColor(Color color){
        light.color = color;
    }
}
