using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    GameObject target;
    [SerializeField]
    GameObject[] obj;
    int pos;
    float time;

    private void Start()
    {
        pos = 0;
        target = obj[0];
        time = 0;
    }

    void FixedUpdate()
    {
        transform.LookAt(target.transform);
        transform.Translate(Vector3.right * Time.deltaTime * 5);
        time += Time.deltaTime;
        if(time >= 2.5f)
        {
            time = 0;
            Destroy(obj[pos]);
            pos++;
            target = obj[pos];
            obj[pos].SetActive(true);
        }
    }
}
