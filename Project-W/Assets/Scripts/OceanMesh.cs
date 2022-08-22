using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class OceanMesh : MonoBehaviour
{
    static Vector3[] vertices;
    static int[] triangles;
    static Vector2[] uvs;
    public static void renderMesh(int xSize, int zSize, float squareScale, Mesh mesh)
    {
        vertices = new Vector3[(xSize + 1) * (zSize + 1)];
        triangles = new int[xSize * zSize * 6];
        uvs = new Vector2[(xSize + 1) * (zSize + 1)];

        CreateShape(xSize, zSize, squareScale);
        UpdateMesh(mesh);
    }

    static void CreateShape(int xSize, int zSize, float squareScale)
    {
        for (int i = 0, z = 0; z <= zSize; z++)
        {
            for (int x = 0; x <= xSize; x++)
            {
                vertices[i] = new Vector3(x * squareScale, 0, z * squareScale);
                uvs[i] = new Vector2(((float)(x + (xSize / 2)) / xSize), ((float)(z + (zSize / 2)) / zSize));
                i++;
            }
        }

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

    static void UpdateMesh(Mesh mesh)
    {
        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.uv = uvs;
        mesh.RecalculateNormals();
    }
}
