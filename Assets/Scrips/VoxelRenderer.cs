using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class VoxelRenderer : MonoBehaviour
{

    private Mesh mesh;
    private List<Vector3> vertices;
    private List<int> triangles;

    private List<Color> colors;

    private int numColors = 0;

    public float size = 1;
    public Vector3 offset = Vector3.zero;
    void Awake()
    {
        mesh = GetComponent<MeshFilter>().mesh;
    }

    // Start is called before the first frame update
    void Start()
    {
        colors = new List<Color>();
        vertices = new List<Vector3>();
        triangles = new List<int>();

        GenerateVoxels();
        UpdateMesh();
    }

    void GenerateVoxels()
    {
        for (int x = 0; x < VoxelData.Width; x++)
        {
            for(int y = 0; y < VoxelData.Height; y++){
                for (int z = 0; z < VoxelData.Depth; z++)
                {
                    if (VoxelData.GetCell(x, y, z) == 0)
                    {
                        continue;
                    }

                    MakeCube(new Vector3(x, y, z));
                }
            }
        }
    }

    void MakeCube(Vector3 position)
    {
        MakeFace(Direction.Up, position);
        MakeFace(Direction.Down, position);
        MakeFace(Direction.North, position);
        MakeFace(Direction.South, position);
        MakeFace(Direction.East, position);
        MakeFace(Direction.West, position);
    }

    void MakeFace(Direction dir, Vector3 position)
    {
        if (VoxelData.GetNeighbor(position, dir) != 0){
                Debug.Log("Skipping face " + dir);
                return;
        }

        int vCount = vertices.Count;

        triangles.Add(vCount + 0);
        triangles.Add(vCount + 1);
        triangles.Add(vCount + 2);
        triangles.Add(vCount + 0);
        triangles.Add(vCount + 2);
        triangles.Add(vCount + 3);

        Vector3[] newVertices = CubeData.faceVertices(dir, position + offset, size);
        vertices.AddRange(newVertices);

        for (int i = 0; i < newVertices.Length; i++)
        {
            colors.Add(
                new Color(
                    Random.Range(0, 255) / 255f,
                    Random.Range(0, 255) / 255f,
                    Random.Range(0, 255) / 255f
                )
            );
        }
    }
    void UpdateMesh()
    {
        // mesh.Clear();

        mesh.vertices = vertices.ToArray();
        mesh.triangles = triangles.ToArray();
        mesh.RecalculateNormals();

        if (numColors != vertices.Count)
        {
            mesh.colors = colors.ToArray();
            numColors = vertices.Count;
        }
    }
}
