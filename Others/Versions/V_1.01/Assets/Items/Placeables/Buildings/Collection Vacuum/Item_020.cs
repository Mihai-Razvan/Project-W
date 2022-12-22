using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_020 : MonoBehaviour      //collection vacuum
{
    [SerializeField]
    LayerMask resourceMask;

    [SerializeField]
    int[] prefabsItemCodes;                     //used to spawn resources prefabs
    [SerializeField]
    GameObject[] resourcesPrefabs;                //used to spawn resources on load from file

    List<GameObject> resourcesObjects;            //this one won't be saved in save data
    List<int> collectedResourceCodes;                //the item codes of the resources collect by the collection vacuum
    List<int[]> resourceItemCodeArray;      //the array of the item codes of the resources collect by the collection vacuum (the array for each resource in resourcesData script
    List<int[]> resourceQuantityArray;
    List<float[]> resourceChargeArray;

    void Awake()  //we use awake because if we use start it is called before load from the save file
    {
        resourcesObjects = new List<GameObject>();
        collectedResourceCodes = new List<int>();
        resourceItemCodeArray = new List<int[]>();
        resourceQuantityArray = new List<int[]>();
        resourceChargeArray = new List<float[]>();
    }

    void Update()
    {
        if (Interactions.getInRangeBuilding() == this.gameObject && ActionLock.getActionLock().Equals("UNLOCKED"))
        {
            if(Input.GetKeyDown(KeyCode.E))
                collect();

            buttonHintHandle();
        }
    }

    void collect()
    {
        for(int i = 0; i < collectedResourceCodes.Count; i++)
        {
            for (int j = 0; j < resourceItemCodeArray[i].Length; j++)
                Player_Inventory.getPlayerInventoryHolder().GetComponent<Inventory>().addItem(resourceItemCodeArray[i][j], resourceQuantityArray[i][j], resourceQuantityArray[i][j]);

            Destroy(resourcesObjects[i]);
        }

        resourcesObjects.Clear();
        collectedResourceCodes.Clear();
        resourceItemCodeArray.Clear();
        resourceQuantityArray.Clear();
        resourceChargeArray.Clear();
    }


    void OnTriggerEnter(Collider collider)
    {
        if(collider.gameObject.layer == 6 && collider.gameObject.TryGetComponent(out Rigidbody rb))     //resource; if gameobject no longer has collider it means is tracted by grappler
        {
            collider.transform.position = this.transform.position;
            Destroy(collider.gameObject.GetComponent<Rigidbody>());
            Destroy(collider.gameObject.GetComponent<BoxCollider>());
            resourcesObjects.Add(collider.gameObject);

            collectedResourceCodes.Add(ItemsList.getItemCode(collider.gameObject.tag));
            resourceItemCodeArray.Add(collider.GetComponent<ResourcesData>().getItemCodeArray());
            resourceQuantityArray.Add(collider.GetComponent<ResourcesData>().getQuantityArray());
            resourceChargeArray.Add(collider.GetComponent<ResourcesData>().getChargeArray());
        }
    }

    void buttonHintHandle()
    {
        int count = resourcesObjects.Count;

        if(count != 0)
        {
            string text = "Collect " + count + " item";
            if (count > 1)
                text += "s";

            Button_Hint.setBuildingInteractionHint(text);
        }
        else
            Button_Hint.clearBuildingInteractionHint();
    }


    public ArrayList getSaveData()
    {
        return new ArrayList() { collectedResourceCodes, resourceItemCodeArray, resourceQuantityArray, resourceChargeArray };
    }

    public void loadData(List<int> collectedResourceCodes, List<int[]> resourceItemCodeArray, List<int[]> resourceQuantityArray, List<float[]> resourceChargeArray)     //used when we are loading this object from file
    {
        this.collectedResourceCodes = collectedResourceCodes;
        this.resourceItemCodeArray = resourceItemCodeArray;
        this.resourceQuantityArray = resourceQuantityArray;
        this.resourceChargeArray = resourceChargeArray;

        for(int i = 0; i < collectedResourceCodes.Count; i++)
        {
            for(int j = 0; j < prefabsItemCodes.Length; j++)
                if(prefabsItemCodes[j] == collectedResourceCodes[i])
                {
                    float ranRot = Random.Range(0, 180);
                    GameObject spawnedObject = Instantiate(resourcesPrefabs[j], this.transform.position, Quaternion.Euler(ranRot, ranRot, ranRot));
                    resourcesObjects.Add(spawnedObject);
                    break;
                }
        }
    }
}
