using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clouds : MonoBehaviour
{
    float speed;
    void Start()
    {
        speed = Random.Range(2, 6);
        float scale = Random.Range(4, 10);
        transform.localScale = new Vector3(scale, scale, scale);
    }

    void Update()
    {
        transform.Translate(transform.forward * speed * Time.deltaTime);
        if (Vector3.Distance(transform.position, new Vector3(0, 90, 0)) > 190)
            Destroy(this.gameObject);
    }
}
