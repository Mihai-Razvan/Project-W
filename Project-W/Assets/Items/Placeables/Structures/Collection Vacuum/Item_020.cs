using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_020 : MonoBehaviour
{
    [SerializeField]
    LayerMask resourceMask;

    void OnTriggerEnter(Collider collider)
    {
        if(collider.gameObject.layer == 6 && collider.gameObject.TryGetComponent(out Rigidbody rb) && !FindObjectOfType<Player>().getActionLock().Equals("INVENTORY_OPENED"))     //resource; if gameobject no longer has collider it means is tracted by grappler
        {
            collider.transform.position = this.transform.position;
            Destroy(collider.gameObject.GetComponent<Rigidbody>());
        }
    }
}
