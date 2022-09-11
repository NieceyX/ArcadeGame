using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    public GameObject buttonHolder;
    public UnityEngine.UI.Button planeButton;
    public UnityEngine.UI.Button truckButton;

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
    public void Trigger()
    {
        Time.timeScale = 0;
        planeButton.onClick.AddListener(planeTask);
        truckButton.onClick.AddListener(truckTask);
        buttonHolder.SetActive(true);
    }
    void planeTask()
    {
        player.GetComponent<PlayerMovement>().Plane();
        buttonHolder.SetActive(false);
        Time.timeScale = 1;
    }

    void truckTask()
    {
        player.GetComponent<PlayerMovement>().Truck();
        buttonHolder.SetActive(false);
        Time.timeScale = 1;
    }
}