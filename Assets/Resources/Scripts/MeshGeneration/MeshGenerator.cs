using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshGenerator : MonoBehaviour
{

    [SerializeField] private int xSize;
    [SerializeField] private int zSize;

    public Vector2 perlinOffset;
    [SerializeField] private float meshSegmentScale = 1f;
    [SerializeField] private BlockDensityManager correspondingBlock;


    public float maxHeight = 0.0f;

    private Mesh mesh;
    private Vector3[] vertices;
    private int[] triangles;
    private Color[] colors;

    private Vector2 targetPos;

    private float initialY;
    private float newY;

    // Start is called before the first frame update
    void Start()
    {
        initialY = transform.position.y;

        perlinOffset.x = transform.position.x;
        perlinOffset.y = transform.position.z;
        
        targetPos = perlinOffset;
        Events.instance.onFloorClicked += SetTarget;

        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
        
        CreateTerrain();
        UpdateMeshTerrain();
    }
    void OnDestroy(){
        Events.instance.onFloorClicked -= SetTarget;
    }

    private void Update(){
        

        UpdateMesh();
    }

    public void UpdateMesh(){
        
        // perlinOffset.x = transform.position.x;
        // perlinOffset.y = transform.position.z;

        CreateTerrain();
        UpdateMeshTerrain();

    }

    


    private void CreateTerrain(){
        vertices = new Vector3[(xSize+1) * (zSize+1)];
        colors = new Color[(xSize+1) * (zSize+1)];

        Color currentColor = BiomeManager.instance.GetColorOfBiome( correspondingBlock.biome );

        for(int i=0, z=0; z <= zSize; z++){
            for(int x=0; x <= xSize; x++){
                Vector3 zPos = transform.TransformPoint(new Vector3(0f, 0f, z*meshSegmentScale));
                if(zPos.z > 5){
                    newY = initialY - (zPos.z - 5);
                }
                else if(zPos.z < -5){
                    newY = initialY - (-5 - zPos.z);
                }
                else{
                    newY = initialY;
                }
                float y = newY + Mathf.PerlinNoise((perlinOffset.x + x*meshSegmentScale)*.3f, (perlinOffset.y + z*meshSegmentScale)*.3f) * maxHeight;
                vertices[i] = new Vector3(x*meshSegmentScale, y, z*meshSegmentScale);
                
                colors[i] = currentColor;
                i++;
            }
        }

        triangles = new int[xSize*zSize*6];
        int tris=0;
        int vert=0;
        for(int z=0; z < zSize; z++){
            for(int x=0; x < xSize; x++){

                triangles[tris+0] = vert + 0;
                triangles[tris+1] = vert + xSize + 1;
                triangles[tris+2] = vert + 1;
                triangles[tris+3] = vert + 1;
                triangles[tris+4] = vert + xSize + 1;
                triangles[tris+5] = vert + xSize + 2;

                vert++;
                tris += 6;
            }
            vert++;
        }

    }

    void UpdateMeshTerrain(){
        mesh.Clear();

        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.colors = colors;

        mesh.RecalculateNormals();
    }

    void SetTarget(Vector3 pos){
        targetPos = new Vector2(perlinOffset.x - pos.x, perlinOffset.y - pos.z);
    }

}
