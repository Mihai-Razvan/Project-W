using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clouds : MonoBehaviour
{
    float speed;
    void Start()
    {
        speed = Random.Range(5, 15);
        float scale = Random.Range(1, 5);
        transform.localScale = new Vector3(scale, scale, scale);
    }

    void Update()
    {
        transform.Translate(transform.forward * speed * Time.deltaTime);
        if (Vector3.Distance(transform.position, new Vector3(0, 90, 0)) > 70)
            Destroy(this.gameObject);
    }
}
