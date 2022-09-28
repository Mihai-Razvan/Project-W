using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AndrewTate : MonoBehaviour
{
    [SerializeField]
    float speed;

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, FindObjectOfType<Player>().getPosition(), speed * Time.deltaTime);
        transform.LookAt(FindObjectOfType<Player>().getPosition());
    }
}
