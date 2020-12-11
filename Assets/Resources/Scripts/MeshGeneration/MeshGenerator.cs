using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshGenerator : MonoBehaviour
{

    [SerializeField] private int xSize;
    [SerializeField] private int zSize;

    [SerializeField] private float speed = 1.0f;
    public Vector2 perlinOffset;
    [SerializeField] private float meshSegmentScale = 1f;

    public float maxHeight = 0.0f;

    private Mesh mesh;
    private Vector3[] vertices;
    private int[] triangles;
    private Color[] colors;

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

    public void UpdateMesh(){
        
        CreateTerrain();
        UpdateMeshTerrain();
    }

    // Update is called once per frame
    void Update()
    {
        float step =  speed * Time.deltaTime;
        perlinOffset = Vector2.MoveTowards(perlinOffset, targetPos, step);

        // CreateTerrain();
        // UpdateMeshTerrain();
    }


    private void CreateTerrain(){
        vertices = new Vector3[(xSize+1) * (zSize+1)];
        colors = new Color[(xSize+1) * (zSize+1)];

        Color currentColor = Color.white;

        for(int i=0, z=0; z <= zSize; z++){
            for(int x=0; x <= xSize; x++){
                float y = Mathf.PerlinNoise((perlinOffset.x + x*meshSegmentScale)*.3f, (perlinOffset.y + z*meshSegmentScale)*.3f) * maxHeight;
                vertices[i] = new Vector3(transform.position.x + x*meshSegmentScale, y, transform.position.z +  z*meshSegmentScale);
                
                
                if(i%3 == 0){
                    currentColor = BiomeManager.instance.GetColorAtPos(transform.TransformPoint(vertices[i]));//new Color(1.0f, z/(float)zSize, 1.0f, z/(float)zSize);
                }
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

                // Color currentColor = BiomeManager.instance.GetColorAtPos(transform.TransformPoint(vertices[vert]));
                // colors[vert + 0] = currentColor;
                // colors[vert + xSize + 1]= currentColor;
                // colors[vert + 1]= currentColor;
                // colors[vert + 1]= currentColor;
                // colors[vert + xSize + 1]= currentColor;
                // colors[vert + xSize + 2]= currentColor;

                vert++;
                tris += 6;
            }
            vert++;
        }

        // for (int i = 0; i < vertices.Length; i++) {
        //     // Makes every vertex unique
        //     // triangles[i] = i;
        //     // Every third vertex randomly chooses new color
        //     if(i % 3 == 0)
        //         currentColor = BiomeManager.instance.GetColorAtPos(transform.TransformPoint(vertices[i]));
        //     colors[i] = currentColor;
        // }

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
