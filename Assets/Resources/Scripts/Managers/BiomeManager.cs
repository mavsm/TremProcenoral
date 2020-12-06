using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BiomeManager : MonoBehaviour
{
    //This is a Singleton, so we need a static reference to itself
    public static BiomeManager instance;

    [System.Serializable]
    public class BiomeObject {
        public string name;
        public List<BiomeDecoration> decorations = new List<BiomeDecoration>();
    }
    [System.Serializable]
    public class BiomeDecoration {
        public string name;
        public GameObject prefab;
        [Tooltip("Chances of each biome decoration appearing.\nRemember that the sum needs to equals 1.")]
        public List<float> chanceArray = new List<float>();
    }


    [SerializeField] private int minObjectsToSwitch = 50;


    [Header("Generation Items")]
    [SerializeField] private List<BiomeObject> generationMatrix = new List<BiomeObject>();
   
    private int currentBiome = 0;
    private int lastGeneratedItemIndex = 0;
    private int currentGeneratedItemsCount = 0;

    void Awake()
    {
        instance = this;
    }

    public GameObject GetDecoration(){
        if(currentGeneratedItemsCount > minObjectsToSwitch){
            if(Random.value <= .5){
                //change biome
                currentGeneratedItemsCount = 0;
                currentBiome = (currentBiome+1)%2; //since rn we only have 2 biomes
            }
        }
        BiomeObject biome = generationMatrix[currentBiome];
        BiomeDecoration lastDecoration = biome.decorations[lastGeneratedItemIndex];
        float value = Random.value;
        for(int i = 0; i < lastDecoration.chanceArray.Count; i++){
            if(value < lastDecoration.chanceArray[i]){
                lastGeneratedItemIndex = i;
                break;
            }
            value -= lastDecoration.chanceArray[i];
        }

        currentGeneratedItemsCount += 1;
        return biome.decorations[lastGeneratedItemIndex].prefab;
    }


}
