using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class CubeData : MonoBehaviour
{
    public static Vector3[] vertices = new Vector3[] {
        new Vector3(0, 1, 0),
        new Vector3(0, 1, 1),
        new Vector3(1, 1, 1),
        new Vector3(1, 1, 0),

        new Vector3(0, 0, 0),
        new Vector3(0, 0, 1),
        new Vector3(1, 0, 1),
        new Vector3(1, 0, 0),
    };

    public static int[][] faceTriangles = {
        // Top
       new int [] {0, 1, 2, 3},
       // Front
       new int [] {4, 0, 3, 7},
       // Back
       new int [] {5, 6, 2, 1},
       // Left
       new int [] {5, 1, 0, 4},
       // Right
       new int [] {7, 3, 2, 6},

       // Bottom
       new int [] {6, 5, 4, 7},
    };

    public static Vector3[] FaceVertices(int face, float size = 1)
    {
        Vector3[] faceValues = new Vector3[4];
        for (int i = 0; i < faceValues.Length; i++)
        {
            faceValues[i] = vertices[faceTriangles[face][i]] * size;
        }

        return faceValues;
    }
    public static Vector3[] FaceVertices(int face, Vector3 offset, float size = 1)
    {
        Vector3[] faceValues = new Vector3[4];
        for (int i = 0; i < faceValues.Length; i++)
        {
            faceValues[i] = (vertices[faceTriangles[face][i]] + offset) * size;
        }

        return faceValues;
    }

    void Update()
    {
        Mesh mesh = GetComponent<MeshFilter>().mesh;

        mesh.vertices = vertices;

        mesh.triangles = new int[] {
            // Top
            0, 1, 2, 2, 3, 0,

            // Bottom
            6, 5, 4, 4, 7, 6
        };

        Color[] colors = new Color[vertices.Length];

        colors[0] = new Color(0, 0, 0); // Black
        colors[1] = new Color(0, 0, 1); // Blue
        colors[2] = new Color(0, 1, 0); // Green
        colors[3] = new Color(0, 1, 1); // Cyan
        colors[4] = new Color(1, 0, 0); // Red
        colors[5] = new Color(1, 0, 1); // Violet
        colors[6] = new Color(1, 1, 0); // Yellow
        colors[7] = new Color(1, 1, 1); // White

        mesh.colors = colors;
    }
}
