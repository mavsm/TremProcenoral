using System.Collections;
using System.Collections.Generic;
using UnityEngine;



/*
    Should get the desired object from somewhere else. A controller that has a list of prefabs per biome
*/


public class BlockDensityManager : MonoBehaviour
{
    [Header("Generation Variables")]
    [SerializeField] public Vector2 noise_initial_position_offset;
    [SerializeField] public Vector2 area_size = new Vector2(10, 10);
    [SerializeField] float x_step = 0.1f;
    [SerializeField] float y_step = 0.1f;
    [SerializeField] float max_chance = 0.8f;

    public string biome;


    private Dictionary<string, int> biomeNum = new Dictionary<string, int>();

    // Start is called before the first frame update
    void Start()
    {
        BiomeManager.instance.biomeBlocks.Add(this);
        PopulateArea();
    }


    public void AreaCycled(Vector2 difference){
        ClearChildren();
        noise_initial_position_offset += difference;
        PopulateArea();
    }

    private void ClearChildren(){
        foreach (Transform child in transform) {
           GameObject.Destroy(child.gameObject);
        }
        biomeNum.Clear();
    }

    private void PopulateArea()
    {
        for(float x_pos = 0f; x_pos < area_size.x; x_pos += x_step){
            float perlin_x = noise_initial_position_offset.x + x_pos;
            for(float y_pos = 0f; y_pos < area_size.y; y_pos += y_step) {
                float perlin_y = noise_initial_position_offset.y + y_pos;
                
                if (Random.value <= max_chance*Mathf.PerlinNoise(perlin_x, perlin_y)){
                    InstantiateObject(x_pos, y_pos);
                }

            }
        }
        foreach(string key in biomeNum.Keys){
            if(biomeNum[key] > biomeNum[biome]){
                biome = key;
            }
        }
    }

    void InstantiateObject(float x, float y)
    {
        GameObject obj = Instantiate(BiomeManager.instance.GetDecoration(), transform);
        string gennedBiome = BiomeManager.instance.GetLastChosenBiome();
        if(biomeNum.ContainsKey(gennedBiome)){
            biomeNum[gennedBiome]++;
        }
        else{
            biome = gennedBiome;
            biomeNum.Add(gennedBiome, 1);
        }
        obj.transform.position = new Vector3(transform.position.x + x, 0f, transform.position.z + y);
    }

    public bool HasPoint(Vector3 pos){
        if(pos.x >= transform.position.x && pos.x < transform.position.x + area_size.x &&
            pos.z >= transform.position.z && pos.z < transform.position.z + area_size.y){
                return true;
        }
        return false;
    }

}
