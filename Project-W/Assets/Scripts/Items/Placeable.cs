using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Placeable : Item
{
    [SerializeField]
    protected GameObject dummyPrefab;    //the prefab that will be used as usedObject 
    [SerializeField]
    protected GameObject placePrefab;        //the prefab that will be spawned when you place it
    [SerializeField]
    protected UnityEngine.Material placeableMaterial;
    [SerializeField]
    protected UnityEngine.Material notPlaceableMaterial;
    [SerializeField]
    protected LayerMask buildingMask;
    
    protected void displayPrefab()
    {
        if (getUsedObject() == null && checkSelected())
        {
            GameObject spawnedObject = Instantiate(dummyPrefab, new Vector3(0, 0, 0), Quaternion.identity);
            setUsedObject(spawnedObject, itemCode);
        }
    }

    protected void spawnPlacePrefab()   //used to place the building when you click
    {
        Instantiate(placePrefab, getUsedObject().transform.position, getUsedObject().transform.rotation);
        int slot = FindObjectOfType<Player_Inventory>().getSelectedSlot();
        FindObjectOfType<Player_Inventory>().getPlayerInventoryHolder().GetComponent<Inventory>().decreaseQuantity(1, slot);
    }

    protected void changeMaterials(Material material)
    {
        for(int child = 0; child < getUsedObject().transform.childCount; child++)
        {
            Renderer usedObjectRenderer = getUsedObject().transform.GetChild(child).gameObject.GetComponent<Renderer>();
            Material[] materials = new Material[usedObjectRenderer.materials.Length];

            for (int i = 0; i < materials.Length; i++)
                materials[i] = material;

            usedObjectRenderer.materials = materials;
        }
    }
}
