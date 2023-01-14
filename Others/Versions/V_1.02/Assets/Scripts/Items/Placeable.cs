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
        if (itemCode == selectedItemCode && getUsedObject() == null)
        {
            GameObject spawnedObject = Instantiate(dummyPrefab, new Vector3(0, 0, 0), Quaternion.identity);
            setUsedObject(spawnedObject, itemCode);
        }
    }

    protected void spawnPlacePrefab()   //used to place the building when you click
    {
        Instantiate(placePrefab, getUsedObject().transform.position, getUsedObject().transform.rotation);
        int slot = Player_Inventory.getSelectedSlot();
        Player_Inventory.getPlayerInventoryHolder().GetComponent<Inventory>().decreaseQuantity(1, slot);

        FindObjectOfType<SoundsManager>().playPlacePlaceableSound();
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

            for (int grandChild = 0; grandChild < getUsedObject().transform.GetChild(child).childCount; grandChild++)       //we did this initially for the chest to be able to do its animation on dummy
            {
                Renderer usedObjectRenderer2 = getUsedObject().transform.GetChild(child).GetChild(grandChild).gameObject.GetComponent<Renderer>();
                Material[] materials2 = new Material[usedObjectRenderer2.materials.Length];

                for (int i = 0; i < materials2.Length; i++)
                    materials2[i] = material;

                usedObjectRenderer2.materials = materials2;
            }
        }
    }
}