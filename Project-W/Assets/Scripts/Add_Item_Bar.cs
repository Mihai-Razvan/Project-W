using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Add_Item_Bar : MonoBehaviour      //used to manage the bars that show when you add an item in the bottom right corner
{
    [SerializeField]
    Transform canvasTransform;
    List<GameObject> barsList;
    List<float> timeSinceInstantiated;
    [SerializeField]
    GameObject barPrefab;
    [SerializeField]
    Transform bottomBarDummy;         //this is a dummy used for position reference
    [SerializeField]
    float moveSpeed;
    float bottomBarDummyYPos;
    float bottomBarDummyXPos;
    float barHeight;
    [SerializeField]
    float decreaseOpacityPerSec;
    
    void Start()
    {
        barsList = new List<GameObject>();
        timeSinceInstantiated = new List<float>();
        bottomBarDummyYPos = bottomBarDummy.GetComponent<RectTransform>().localPosition.y;
        bottomBarDummyXPos = bottomBarDummy.GetComponent<RectTransform>().localPosition.x;
        barHeight = barPrefab.GetComponent<RectTransform>().sizeDelta.y;
    }

    void Update()
    {
        moveBars();
        fadeBars();
    }

    public void addBar(int itemCode, int quantity)
    {
        float posY = bottomBarDummyYPos - barHeight;
        GameObject spawnedObject = Instantiate(barPrefab);
        spawnedObject.transform.SetParent(canvasTransform);
        spawnedObject.GetComponent<RectTransform>().localPosition = new Vector3(bottomBarDummyXPos, posY, 0);
        spawnedObject.transform.localScale = new Vector3(1, 1, 1);

        spawnedObject.transform.Find("Quantity").gameObject.GetComponent<TextMeshProUGUI>().text = "+" + quantity;
        spawnedObject.transform.Find("Item_Name").gameObject.GetComponent<TextMeshProUGUI>().text = ItemsList.getName(itemCode);
        spawnedObject.transform.Find("Item_Image").gameObject.GetComponent<Image>().sprite = ItemsList.getSprite(itemCode);

        barsList.Insert(0, spawnedObject);
        timeSinceInstantiated.Insert(0, 0);
    }

    void moveBars()
    {
        for (int i = 0; i < barsList.Count; i++)
        {
            float targetPosition = bottomBarDummyYPos + i * barHeight;
            float actualPosition = barsList[i].GetComponent<RectTransform>().localPosition.y;

            if (actualPosition < targetPosition)
            {
                barsList[i].GetComponent<RectTransform>().localPosition = new Vector3(bottomBarDummyXPos, actualPosition + moveSpeed * Time.deltaTime, 0);
                if(barsList[i].GetComponent<RectTransform>().localPosition.y > targetPosition)
                    barsList[i].GetComponent<RectTransform>().localPosition = new Vector3(bottomBarDummyXPos, targetPosition, 0);
            }
        }
    }

    void fadeBars()
    {
        for(int i = 0; i < barsList.Count; i++)
        {
            timeSinceInstantiated[i] += Time.deltaTime;
            if(timeSinceInstantiated[i] >= 5)       //disappear
            {
                Destroy(barsList[i].gameObject);
                barsList.RemoveAt(i);
                timeSinceInstantiated.RemoveAt(i);
                i--;
                continue;
            }
            else if(timeSinceInstantiated[i] >= 2)        //fade
            {
                Color newColor = new Color(255, 255, 255, barsList[i].transform.Find("Background").GetComponent<Image>().color.a - decreaseOpacityPerSec * Time.deltaTime);
                barsList[i].transform.Find("Background").GetComponent<Image>().color = newColor;
                barsList[i].transform.Find("Quantity").GetComponent<TextMeshProUGUI>().color = newColor;
                barsList[i].transform.Find("Item_Image").GetComponent<Image>().color = newColor;
                barsList[i].transform.Find("Item_Name").GetComponent<TextMeshProUGUI>().color = newColor;
            }
        }
    }
}
