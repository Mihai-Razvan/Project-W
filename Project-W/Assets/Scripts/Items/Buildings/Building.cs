using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : Item
{
    [SerializeField]
    protected GameObject dummyPrefab;    //the prefab that will be used as usedObject 
    [SerializeField]
    protected GameObject placePrefab;        //the prefab that will be spawned when you place it
    [SerializeField]
    protected Material placeableMaterial;
    [SerializeField]
    protected Material notPlaceableMaterial;
    [SerializeField]
    protected LayerMask buildingMask;
    
    protected void displayPrefab()
    {
        if (getUsedObject() == null && checkSelected())
        {
            GameObject spawnedObject = Instantiate(dummyPrefab, new Vector3(0, 0, 0), Quaternion.identity);
            setUsedObject(spawnedObject);
        }
    }

    protected void placeBuilding()
    {
        Instantiate(placePrefab, getUsedObject().transform.position, getUsedObject().transform.rotation);
        int slot = FindObjectOfType<Player_Inventory>().getSelectedSlot();
        FindObjectOfType<Player_Inventory>().getPlayerInventoryHolder().GetComponent<Inventory>().decreaseQuantity(1, slot);
    }

    protected void changeMaterials(Material material)
    {
        Renderer usedObjectRenderer = getUsedObject().transform.GetChild(0).gameObject.GetComponent<Renderer>();
        Material[] materials = new Material[usedObjectRenderer.materials.Length];

        for (int i = 0; i < materials.Length; i++)
            materials[i] = material;

        usedObjectRenderer.materials = materials;
    }
}
