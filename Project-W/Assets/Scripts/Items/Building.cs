using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : Placeable
{
    [SerializeField]
    float checkFoundationDistance;
    protected void placeDummy()   //used to place the dummy
    {
        RaycastHit[] hits = Physics.RaycastAll(Camera.main.transform.position, Camera.main.transform.forward, checkFoundationDistance, buildingMask);
        for (int i = 0; i < hits.Length; i++)
            if (hits[i].collider.TryGetComponent(out Placeable_Data data) && data.getStructureType().Equals("Foundation"))
            {
                float yPos = hits[i].collider.transform.position.y + hits[i].collider.GetComponent<BoxCollider>().size.y / 2;
                getUsedObject().transform.position = new Vector3(hits[i].point.x, yPos, hits[i].point.z);
                break;
            }
    }

    protected void rotateObject()
    {
        if (Input.GetKey(KeyCode.R))
            getUsedObject().transform.rotation *= Quaternion.Euler(0, 90 * Time.deltaTime, 0);
    }
}
