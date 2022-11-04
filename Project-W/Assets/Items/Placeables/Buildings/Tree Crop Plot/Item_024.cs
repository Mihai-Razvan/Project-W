using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_024 : MonoBehaviour
{
    [SerializeField]
    GameObject treePlace;
    [SerializeField]
    int[] seedsItemCodes;   //item codes for seeds you can plant in this tree crop plot
    [SerializeField]
    GameObject[] treesModels;
    [SerializeField]
    int[] fruitsItemCodes;    //the fruit item codes that grow after planting seed
    [SerializeField]
    GameObject[] fruitsModels;
    [SerializeField]
    int[] growingTimes;
    [SerializeField]
    Material driedMaterial;
    string status;                  //EMPTY, GROWING, GROWN, COLLECTED, DESTROYING
    int fruitItemCode;
    int index;
    float timeSincePlanted;
    float timeSinceStartedCharge;      //when you want to destroy the dree and need to hold mouse to start destroying it
    float timeSinceFall;
    float sizeGrowPerSec;              //how much the model scale grows
    GameObject treeObject;


    void Start()
    {
        status = "EMPTY";
    }

    void Update()
    {
        if (FindObjectOfType<Interactions>().getInRangeBuilding() == this.gameObject && !FindObjectOfType<Player>().getActionLock().Equals("INVENTORY_OPENED"))
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                interaction();
            }
        }

        switch (status)
        {
            case "GROWING":
                grow();
                break;
            case "DESTROYING":
                fall();
                break;
            case "COLLECTED":
                if (Input.GetKeyUp(KeyCode.E) || FindObjectOfType<Interactions>().getInRangeBuilding() != this.gameObject && timeSinceStartedCharge != 0)
                {
                    timeSinceStartedCharge = 0;
                    FindObjectOfType<ChargeRadial>().resetCharge();
                }
                else if (Input.GetKey(KeyCode.E))
                    destroyTree();
                break;
        }
    }

    void interaction()
    {
        switch (status)
        {
            case "EMPTY":
                if (getIndex(FindObjectOfType<Player_Inventory>().getSelectedItemCode()) != -1)
                    plant();
                break;
            case "GROWN":
                collectFruits();
                break;
            case "COLLECTED":
                timeSinceStartedCharge = 0;
                break;
        }
    }

    void plant()
    {
        index = getIndex(FindObjectOfType<Player_Inventory>().getSelectedItemCode());
        int playerInventorySlot = FindObjectOfType<Player_Inventory>().getSelectedSlot();
        FindObjectOfType<Player_Inventory>().getPlayerInventoryHolder().GetComponent<Inventory>().decreaseQuantity(1, playerInventorySlot);
        treeObject = Instantiate(treesModels[index], treePlace.transform.position, Quaternion.identity);
        treeObject.transform.SetParent(treePlace.transform);
        treeObject.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        fruitItemCode = fruitsItemCodes[index];
        timeSincePlanted = 0;
        status = "GROWING";
    }

    void grow()
    {
        timeSincePlanted += Time.deltaTime;
        float scale = (timeSincePlanted / growingTimes[index]) * 0.9f + 0.1f;
        treeObject.transform.localScale = new Vector3(scale, scale, scale);

        if(timeSincePlanted >= growingTimes[index])
        {
            status = "GROWN";
            GameObject fruitsParent = treeObject.transform.Find("Fruits-Places").gameObject;
            for (int i = 0; i < fruitsParent.transform.childCount; i++)
            {
                GameObject spawnedFruit = Instantiate(fruitsModels[index], fruitsParent.transform.GetChild(i).transform.position, fruitsParent.transform.GetChild(i).transform.rotation);
                spawnedFruit.transform.parent = fruitsParent.transform.GetChild(i).transform;
            }
        }
    }

    void collectFruits()
    {
        if(treePlace.transform.GetChild(0).Find("Fruits-Places").transform.childCount == 1)     //there are no fruits left
        {
            status = "COLLECTED";
            Renderer usedObjectRenderer = treeObject.transform.GetChild(0).gameObject.GetComponent<Renderer>();
            Material[] materials = new Material[usedObjectRenderer.materials.Length];

            for (int i = 0; i < materials.Length; i++)
                materials[i] = driedMaterial;

            usedObjectRenderer.materials = materials;
        }

        FindObjectOfType<Player_Inventory>().getPlayerInventoryHolder().GetComponent<Inventory>().addItem(fruitItemCode, 1);
        Destroy(treePlace.transform.GetChild(0).Find("Fruits-Places").GetChild(0).gameObject);  //the plant
    }

    void destroyTree()
    {
        timeSinceStartedCharge += Time.deltaTime;
        FindObjectOfType<ChargeRadial>().setCharge(timeSinceStartedCharge, 1);
    
        if (timeSinceStartedCharge >= 1)
        {
            status = "DESTROYING";
            treeObject.AddComponent<Rigidbody>();
            timeSinceFall = 0;
            timeSinceStartedCharge = 0;
            FindObjectOfType<ChargeRadial>().resetCharge();
        }
    }

    void fall()
    {
        timeSinceFall += Time.deltaTime;
        if (timeSinceFall >= 2)
        {
            Destroy(treeObject);
            status = "EMPTY";
        }
    }

    int getIndex(int itemCode)
    {
        for (int i = 0; i < seedsItemCodes.Length; i++)
            if (seedsItemCodes[i] == itemCode)
                return i;

        return -1;
    }
}
