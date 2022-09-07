using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DestroyOnCollision : MonoBehaviour
{
    void Start()
    {

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(collision.gameObject);
        if (collision.gameObject.layer == 3)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }   
    }
}
