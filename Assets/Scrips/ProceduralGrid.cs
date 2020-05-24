using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(MeshFilter), typeof(MeshRenderer))]
public class ProceduralGrid : MonoBehaviour
{

    Mesh mesh;
    Vector3[] vertices;
    int[] triangles;

    // Grid settings
    public float cellSize;
    public Vector3 gridOffset;
    public int gridSize;

    void Awake()
    {
        mesh = GetComponent<MeshFilter>().mesh;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }
}
