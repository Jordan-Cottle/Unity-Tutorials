using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class ProceduralGrid : MonoBehaviour
{

    Mesh mesh;
    Vector3[] vertices;
    int[] triangles;

    // Grid settings
    public float cellSize = 1;
    public Vector3 gridOffset;
    public int gridSize = 1;

    void Awake()
    {
        mesh = GetComponent<MeshFilter>().mesh;
    }

    // Start is called before the first frame update
    void Start()
    {
        MakeProceduralGrid();
        UpdateMesh();
    }

    void MakeProceduralGrid()
    {
        // Initialize arrays
        vertices = new Vector3[4 * (gridSize * gridSize)];
        triangles = new int[6 * gridSize];

        // Set vertex offsets
        float vertexOffset = cellSize * 0.5f;

        // Populate arrays
        vertices[0] = new Vector3(-vertexOffset, 0, -vertexOffset) + gridOffset;
        vertices[1] = new Vector3(-vertexOffset, 0, vertexOffset) + gridOffset;
        vertices[2] = new Vector3(vertexOffset, 0, -vertexOffset) + gridOffset;
        vertices[3] = new Vector3(vertexOffset, 0, vertexOffset) + gridOffset;

        triangles[0] = 0;
        triangles[1] = 1;
        triangles[2] = 2;
        triangles[3] = 2;
        triangles[4] = 1;
        triangles[5] = 3;
    }

    void UpdateMesh()
    {
        mesh.Clear();

        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
    }

    void Update()
    {
        MakeProceduralGrid();
        UpdateMesh();
    }

}
