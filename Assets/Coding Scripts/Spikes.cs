using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.transform.tag);
        if (collision.transform.tag == "Player")
        {
            collision.transform.position = new Vector3(-9.46f, -0.14f, -5);
        }
    }
}
