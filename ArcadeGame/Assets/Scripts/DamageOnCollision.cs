using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageOnCollision : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 3)
        {
            GameObject[] items = GameObject.FindGameObjectsWithTag("Bullet");
            foreach(GameObject item in items)
            {
                Destroy(item);
            }
            collision.gameObject.GetComponent<PlayerMovement>().TakeDamage(1);
        }
    }
}
