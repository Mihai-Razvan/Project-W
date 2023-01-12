using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_024 : Item      //kwaki tree
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
    GameObject treeObject;


    void Awake()  //we use awake because if we use start it is called before load from the save file
    {
        status = "EMPTY";
    }

    void Update()
    {
        if (Interactions.getInRangeBuilding() == this.gameObject && ActionLock.getActionLock().Equals("UNLOCKED"))
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                interaction();
            }

            buttonHintHandle();
        }

        switch (status)
        {
            case "GROWING":
                grow();
                break;
            case "DESTROYING":
                dissolve();
                break;
            case "COLLECTED":
                if (Input.GetKeyUp(KeyCode.E) || Interactions.getInRangeBuilding() != this.gameObject && timeSinceStartedCharge != 0)
                {
                    timeSinceStartedCharge = 0;
                    ChargeRadial.resetCharge();
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
                if (getIndex(selectedItemCode) != -1)
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
        index = getIndex(selectedItemCode);
        int playerInventorySlot = Player_Inventory.getSelectedSlot();
        Player_Inventory.getPlayerInventoryHolder().GetComponent<Inventory>().decreaseQuantity(1, playerInventorySlot);
        treeObject = Instantiate(treesModels[index], treePlace.transform.position, this.gameObject.transform.rotation);
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

        if (timeSincePlanted >= growingTimes[index])
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
            treeObject.GetComponent<ReplaceMats>().replaceDry();
        }

        Player_Inventory.getPlayerInventoryHolder().GetComponent<Inventory>().addItem(fruitItemCode, 1, 0);
        Destroy(treePlace.transform.GetChild(0).Find("Fruits-Places").GetChild(0).gameObject);  //the plant
    }

    void destroyTree()
    {
        timeSinceStartedCharge += Time.deltaTime;
        ChargeRadial.setCharge(timeSinceStartedCharge, 1, false);
    
        if (timeSinceStartedCharge >= 1f)
        {
            status = "DESTROYING";
            treeObject.GetComponent<ReplaceMats>().replaceDissolving();
            treeObject.transform.GetChild(0).gameObject.AddComponent<DissolveExample.DissolveChilds>();

            timeSinceFall = 0;
            timeSinceStartedCharge = 0;
            ChargeRadial.resetCharge();
        }
    }

    void dissolve()
    {
        timeSinceFall += Time.deltaTime;
        if (timeSinceFall >= 1.5f)
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

    void buttonHintHandle()
    {
        switch (status)
        {
            case "EMPTY":
                if (getIndex(selectedItemCode) != -1)
                {
                    string text = "Plant '" + ItemsList.getName(selectedItemCode) + "'";
                    Button_Hint.setBuildingInteractionHint(text);
                }
                else
                    Button_Hint.clearBuildingInteractionHint();
                break;
            case "GROWING":
                Button_Hint.clearBuildingInteractionHint();
                break;
            case "GROWN":
                {
                    string text = "Collect '" + ItemsList.getName(fruitItemCode) + "'";
                    Button_Hint.setBuildingInteractionHint(text);
                }
                break;
            case "COLLECTED":
                Button_Hint.setBuildingInteractionHint("Hold until tree is destroyed");
                break;
            case "DESTROYING":
                Button_Hint.clearBuildingInteractionHint();
                break;
        }
    }

    public ArrayList getSaveData()
    {
        return new ArrayList() { status, index, fruitItemCode, timeSincePlanted };
    }

    public void loadData(string status, int index, int fruitItemCode, float timeSincePlanted)     //used when we are loading this object from file
    {
        this.status = status;
        this.index = index;
        this.fruitItemCode = fruitItemCode;
        this.timeSincePlanted = timeSincePlanted;

        if (!status.Equals("EMPTY"))
        {
            treeObject = Instantiate(treesModels[index], treePlace.transform.position, this.gameObject.transform.rotation);
            treeObject.transform.SetParent(treePlace.transform);

            float scale = 1;

            switch (status)
            {
                case "GROWING":
                    scale = Mathf.Min((timeSincePlanted / growingTimes[index]), 1) * 0.9f + 0.1f;
                    break;
                case "GROWN":
                    GameObject fruitsParent = treeObject.transform.Find("Fruits-Places").gameObject;
                    for (int i = 0; i < fruitsParent.transform.childCount; i++)
                    {
                        GameObject spawnedFruit = Instantiate(fruitsModels[index], fruitsParent.transform.GetChild(i).transform.position, fruitsParent.transform.GetChild(i).transform.rotation);
                        spawnedFruit.transform.parent = fruitsParent.transform.GetChild(i).transform;
                    }
                    break;
                case "COLLECTED":
                    treeObject.GetComponent<ReplaceMats>().replaceDry();
                    break;
            }

            treeObject.transform.localScale = new Vector3(scale, scale, scale);
        }
    }
}
