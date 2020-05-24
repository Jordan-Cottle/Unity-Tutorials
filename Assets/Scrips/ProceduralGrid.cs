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
    public int gridWidth = 1;
    public int gridHeight = 1;

    void Awake()
    {
        mesh = GetComponent<MeshFilter>().mesh;
    }

    // Start is called before the first frame update
    void Update()
    {
        MakeDiscreteProceduralGrid();
        UpdateMesh();
    }

    void MakeDiscreteProceduralGrid()
    {
        // Initialize arrays
        int numCells = gridWidth * gridHeight;
        vertices = new Vector3[4 * numCells];
        triangles = new int[6 * numCells];

        // Set vertex offsets
        float vertexOffset = cellSize * 0.5f;

        // Populate arrays

        int vertex = 0;
        int triangle = 0;

        Vector3 cellOffset;
        for (int x = 0; x < gridWidth; x++)
        {
            for (int y = 0; y < gridHeight; y++)
            {
                cellOffset = new Vector3(x * cellSize, 0, y * cellSize) + gridOffset;

                int height = x + y;
                vertices[vertex] = new Vector3(-vertexOffset, height, -vertexOffset) + cellOffset;
                vertices[vertex + 1] = new Vector3(-vertexOffset, height, vertexOffset) + cellOffset;
                vertices[vertex + 2] = new Vector3(vertexOffset, height, -vertexOffset) + cellOffset;
                vertices[vertex + 3] = new Vector3(vertexOffset, height, vertexOffset) + cellOffset;

                triangles[triangle] = vertex;
                triangles[triangle + 1] = vertex + 1;
                triangles[triangle + 2] = vertex + 2;
                triangles[triangle + 3] = vertex + 2;
                triangles[triangle + 4] = vertex + 1;
                triangles[triangle + 5] = vertex + 3;

                vertex += 4;
                triangle += 6;
            }
        }

    }

    void UpdateMesh()
    {
        mesh.Clear();

        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
    }
}
