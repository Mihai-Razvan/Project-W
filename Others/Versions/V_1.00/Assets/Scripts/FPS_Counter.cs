using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FPS_Counter : MonoBehaviour
{
    [SerializeField]
    public TextMeshProUGUI fpsText;
    public float deltaTime;


    void Update()
    {
        deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
        float fps = 1.0f / deltaTime;
        fpsText.text = Mathf.Ceil(fps).ToString();
        if (fps <= 40)
            fpsText.color = Color.red;
        else if (fps < 50)
            fpsText.color = Color.yellow;
        else
            fpsText.color = Color.green;
    }
}
