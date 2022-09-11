using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneAI : MonoBehaviour
{
    GameObject projectile;
    public int shootTime = 5;
    public int destroyTime = 2;
    float sinceShot = 0;
    GameObject bullet = null;
    public float projectileSpeed = -15f;

    public float speedOffset = 30f;
    public float runSpeed = 1.1f;

    // Start is called before the first frame update
    void Start()
    {
        projectile = (GameObject)Resources.Load("Prefabs/EnemyProjectile", typeof(GameObject));
    }

    // Update is called once per frame
    void Update()
    {

        sinceShot += Time.deltaTime;
        if (sinceShot >= destroyTime && bullet != null)
        {
            Destroy(bullet);
        }
        if (sinceShot >= shootTime)
        {
            //shoot
            sinceShot = 0;
            bullet = Instantiate(projectile, this.transform.position, Quaternion.identity);
            bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);
        }
    }
    private void FixedUpdate()
    {
        //player movement
        this.transform.position = new Vector2(this.transform.position.x +runSpeed / speedOffset, this.transform.position.y);

    }
}
