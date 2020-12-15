using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainLightManager : MonoBehaviour
{
    [SerializeField] private float changeDuration = 0.5f;

    private Vector3 posToLookAt = new Vector3();
    private Light light;

    private bool mode = false;

    void Start(){
        light = GetComponent<Light>();
    }

    
    // Update is called once per frame
    void Update()
    {
        if(!LeanTween.isTweening(gameObject) && mode != BiomeManager.instance.GetTrainLightModeAtPos(posToLookAt)){
            float newMode = 1.0f;
            if(mode) newMode = 0.0f;
            LeanTween.value(gameObject, changeIntensity, light.intensity, newMode, changeDuration);
            mode = !mode;
        }
    }

    private void changeIntensity(float val){
        light.intensity = val;
    }
}
