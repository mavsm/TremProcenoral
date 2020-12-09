using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshGenerator : MonoBehaviour
{

    [SerializeField] private int xSize;
    [SerializeField] private int zSize;

    [SerializeField] private float speed = 1.0f;
    public Vector2 perlinOffset;

    public float maxHeight = 0.0f;

    private Mesh mesh;
    private Vector3[] vertices;
    private int[] triangles;

    private Vector2 targetPos;

    // Start is called before the first frame update
    void Start()
    {
        
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


    // Update is called once per frame
    void Update()
    {
        float step =  speed * Time.deltaTime;
        perlinOffset = Vector2.MoveTowards(perlinOffset, targetPos, step);

        CreateTerrain();
        UpdateMeshTerrain();
    }


    private void CreateTerrain(){
        vertices = new Vector3[(xSize+1) * (zSize+1)];

        for(int i=0, z=0; z <= zSize; z++){
            for(int x=0; x <= xSize; x++){
                float y = Mathf.PerlinNoise((perlinOffset.x + x), (perlinOffset.y + z)) * maxHeight;
                vertices[i] = new Vector3(x, y, z);
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

        mesh.RecalculateNormals();
    }

    void SetTarget(Vector3 pos){
        targetPos = new Vector2(perlinOffset.x - pos.x, perlinOffset.y - pos.z);
    }

}
