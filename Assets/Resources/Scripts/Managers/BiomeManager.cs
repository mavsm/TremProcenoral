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
        public Color groundColor;
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

    public List<BlockDensityManager> biomeBlocks;

    void Awake()
    {
        instance = this;
        biomeBlocks = new List<BlockDensityManager>();
    }

    public string GetLastChosenBiome(){
        return generationMatrix[currentBiome].name;
    }

    public GameObject GetDecoration(){
        if(currentGeneratedItemsCount > minObjectsToSwitch){
            if(Random.value <= .5){
                //change biome
                currentGeneratedItemsCount = 0;
                int prevBiome = currentBiome;
                currentBiome = Random.Range(0, generationMatrix.Count);
                if(prevBiome == currentBiome){
                    currentBiome = (prevBiome+1)%generationMatrix.Count;
                }
            }
        }
        BiomeObject biome = generationMatrix[currentBiome];
        if(lastGeneratedItemIndex >= biome.decorations.Count){
            lastGeneratedItemIndex = 0;
        }
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

    public Color GetColorAtPos(Vector3 pos){
        foreach(BlockDensityManager block in biomeBlocks){
            if(block.HasPoint(pos)){
                foreach(BiomeObject biome in generationMatrix){
                    if(biome.name == block.biome) return biome.groundColor;
                }
            }
        }
        return Color.white;
    }

    public Color GetColorOfBiome( string selectedBiome ){
        foreach(BiomeObject biome in generationMatrix){
            if(selectedBiome == biome.name) return biome.groundColor;
        }
        return Color.white;
    }
}
