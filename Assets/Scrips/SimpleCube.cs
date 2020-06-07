using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class SimpleCube : MonoBehaviour
{

    private Mesh mesh;
    private List<Vector3> vertices;
    private List<int> triangles;

    private List<Color> colors;

    public float size = 1;
    public Vector3 position = new Vector3(0, 0, 0);

    void Awake()
    {
        mesh = GetComponent<MeshFilter>().mesh;
        colors = new List<Color>();
        vertices = new List<Vector3>();
        triangles = new List<int>();

        for (int i = 0; i < 24; i++)
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

    // Start is called before the first frame update
    void Update()
    {
        MakeCube();
        UpdateMesh();
    }

    void MakeCube()
    {
        vertices = new List<Vector3>();
        triangles = new List<int>();


        for (int i = 0; i < 6; i++)
        {
            MakeFace(i);
        }

    }

    void MakeFace(int face)
    {
        int vCount = vertices.Count;

        triangles.Add(vCount + 0);
        triangles.Add(vCount + 1);
        triangles.Add(vCount + 2);
        triangles.Add(vCount + 0);
        triangles.Add(vCount + 2);
        triangles.Add(vCount + 3);

        vertices.AddRange(CubeData.FaceVertices(face, position, size));
    }
    void UpdateMesh()
    {
        mesh.Clear();

        mesh.vertices = vertices.ToArray();
        mesh.triangles = triangles.ToArray();
        mesh.colors = colors.ToArray();
        mesh.RecalculateNormals();
    }
}
