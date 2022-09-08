using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    bool triggered;
    GameObject player;
    void Start()
    {
        triggered = false;
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (!triggered)
        {
            if (collider.gameObject.layer == 3)
            {
                player = collider.gameObject;
                Trigger();
            }
        }
    }
    void Trigger()
    {
        player.GetComponent<PlayerMovement>().Switch();
    }
}