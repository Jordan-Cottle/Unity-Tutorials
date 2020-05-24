using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class ContinuousGrid : MonoBehaviour
{

    Mesh mesh;
    Vector3[] vertices;
    int[] triangles;

    // Grid settings
    public float cellSize = 1;
    public Vector3 gridOffset;
    public int gridWidth = 1;
    public int gridHeight = 1;

    public float gridSlope = 0.0f;

    void Awake()
    {
        mesh = GetComponent<MeshFilter>().mesh;
    }

    // Start is called before the first frame update
    void Update()
    {
        MakeContinuousProceduralGrid();
        UpdateMesh();
    }

    void MakeContinuousProceduralGrid()
    {
        // Initialize arrays
        int numCells = gridWidth * gridHeight;
        // (w+1)(h+1) = wh + 1w + 1h + 1^2;
        vertices = new Vector3[numCells + gridWidth + gridHeight + 1];
        triangles = new int[6 * numCells];

        // Set vertex offsets
        float vertexOffset = 0;

        // Populate arrays
        int vertex = 0;
        int triangle = 0;
        int columnOffset = gridHeight + 1;
        Debug.Log(columnOffset);
        for (int x = 0; x <= gridWidth; x++)
        {
            // Vertical columns built first
            for (int z = 0; z <= gridHeight; z++)
            {
                vertices[vertex] = new Vector3(
                    x * cellSize - vertexOffset,
                    (x + z) * gridSlope,
                    z * cellSize - vertexOffset
                ) + gridOffset;

                // Skip final row/column until a full quad is ready
                if (x != gridWidth && z != gridHeight)
                {
                    // v+1 *----* v + gh + 2
                    //     |  //|
                    //     |//  |
                    //   v *----* v + gh + 1
                    int bottomLeft = vertex;
                    int topLeft = vertex + 1;

                    int bottomRight = vertex + columnOffset;
                    int topRight = vertex + columnOffset + 1;

                    Debug.Log($"Vertex: {vertex}:{vertices[vertex]}");
                    Debug.Log($"BottomRight = {bottomRight}:{vertices[bottomRight]}");
                    Debug.Log($"BottomLeft = {bottomLeft}:{vertices[bottomLeft]}");
                    Debug.Log($"topLeft = {topLeft}:{vertices[topLeft]}");
                    Debug.Log($"topRight = {topRight}:{vertices[topRight]}");

                    // Top left
                    triangles[triangle] = bottomLeft;
                    triangles[triangle + 1] = topLeft;
                    triangles[triangle + 2] = topRight;

                    // Bottom Right
                    triangles[triangle + 3] = bottomLeft;
                    triangles[triangle + 4] = topRight;
                    triangles[triangle + 5] = bottomRight;

                    triangle += 6;
                }

                vertex++;
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
