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
    // [SerializeField] float min_density_multiplier = 0.1f;
    // [SerializeField] float max_density_multiplier = 1;
    [SerializeField] float max_chance = 0.8f;


    [Header("Spawnable GameObjects")]
    [SerializeField] GameObject spawnable_object;

    // Start is called before the first frame update
    void Start()
    {
        PopulateArea();
    }


    void PopulateArea()
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
    }

    void InstantiateObject(float x, float y)
    {
        GameObject obj = Instantiate(BiomeManager.instance.GetDecoration(), transform);
        obj.transform.position = new Vector3(transform.position.x + x, 0f, transform.position.z + y);
    }

}
