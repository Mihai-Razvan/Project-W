using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class Water : MonoBehaviour
{
    [SerializeField]
    int xSize;
    [SerializeField]
    int zSize;
    Mesh mesh;
    Vector3[] vertices;
    int[] triangles;
    Vector2[] uvs;
    float squareScale = 3f;  //used to determine the size of the squares and triangles. If we need lower poly increase the number; if we need more polished decrease the number

    void Start()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
        CreateShape();
        UpdateMesh();
    }

    private void Update()
    {
        Vector3 playerPos = FindObjectOfType<Player>().getPosition();
        transform.position = new Vector3(playerPos.x - (xSize * squareScale) / 2, 0, playerPos.z - (zSize * squareScale) / 2);
    }

    void CreateShape()
    {
        vertices = new Vector3[(xSize + 1) * (zSize + 1)];
        uvs = new Vector2[(xSize + 1) * (zSize + 1)];

        for (int i = 0, z = 0; z <= zSize; z++)
        {
            for (int x = 0; x <= xSize; x++)
            {
                vertices[i] = new Vector3(x * squareScale, 0, z * squareScale);
                uvs[i] = new Vector2(((float)(x + (xSize / 2)) / xSize), ((float)(z + (zSize / 2)) / zSize));
                i++;
            }
        }

        triangles = new int[xSize * zSize * 6];

        int vert = 0;
        int tris = 0;

        for (int z = 0; z < zSize; z++)
        {
            for (int x = 0; x < xSize; x++)
            {
                triangles[tris + 0] = vert + 0;
                triangles[tris + 1] = vert + xSize + 1;
                triangles[tris + 2] = vert + 1;
                triangles[tris + 3] = vert + 1;
                triangles[tris + 4] = vert + xSize + 1;
                triangles[tris + 5] = vert + xSize + 2;

                vert++;
                tris += 6;
            }
            vert++;
        }

    }

    void UpdateMesh()
    {
        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.uv = uvs;
        mesh.RecalculateNormals();
    }
}
