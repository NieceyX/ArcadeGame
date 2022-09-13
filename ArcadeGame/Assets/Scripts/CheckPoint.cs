using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    public GameObject buttonHolder;
    public UnityEngine.UI.Button planeButton;
    public UnityEngine.UI.Button truckButton;

    public bool end;

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
                if (end)
                {
                    buttonHolder.SetActive(true);
                    collider.GetComponent<PlayerMovement>().end = true;
                    Time.timeScale = 0;
                }
                else
                {
                    player = collider.gameObject;
                    Trigger();
                }
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
        //player.GetComponent<PlayerMovement>().ChangeAvata(false);
        buttonHolder.SetActive(false);

    }

    void truckTask()
    {
        player.GetComponent<PlayerMovement>().Truck();
        //player.GetComponent<PlayerMovement>().ChangeAvata(true);
        buttonHolder.SetActive(false);
        Time.timeScale = 1;
    }
}