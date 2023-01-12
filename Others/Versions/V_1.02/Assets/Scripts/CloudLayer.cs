using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudLayer: MonoBehaviour
{
    [SerializeField]
    int xSize;
    [SerializeField]
    int zSize;
    [SerializeField]
    float squareScale;  //used to determine the size of the squares and triangles. If we need lower poly increase the number; if we need more polished decrease the number

    void Start()
    {
        CloudLayerMesh.renderMesh(xSize, zSize, squareScale, GetComponent<MeshFilter>().mesh);
    }

    void Update()
    {
     //   Vector3 playerPos = FindObjectOfType<Player>().getPosition();
     //   transform.position = new Vector3(playerPos.x - (xSize * squareScale) / 2, 0, playerPos.z - (zSize * squareScale) / 2);
    }
}
