using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //health bar

    public int maxHealth = 3;
    public int currentHealth;

    public HealthBar healthBar;

    //fuel bar

    public int maxFuel = 5;
    public int currentFuel;

    public FuelBar fuelBar;

    Rigidbody2D body;

    float horizontal;

    //jump parameters
    public float buttonTime = 0.5f;
    public float jumpHeight = 5;
    float jumpTime = 100f;
    float sinceJump = 100;
    public float jumpCooldown = 3f;
    bool jumping;

    public float runSpeed = 1.1f;

    //camera parameters
    public Camera camera;
    public int CameraOffset = 5;

    //projectiles
    public GameObject projectile;
    public float projectileSpeed = 20f;
    public float shotCooldown = 3f;
    float sinceShot = 100;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();

        //healthbar
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        //timers
        sinceJump += Time.deltaTime;
        sinceShot += Time.deltaTime;

        //check for left/right input
        horizontal = Input.GetAxisRaw("Horizontal") + runSpeed;

        //jumping code -- jump slightly longer/higher for a longer press
        if (Input.GetKeyDown(KeyCode.Space) && jumpCooldown < sinceJump)
        {
            sinceJump = 0;
            jumping = true;
            jumpTime = 0;
        }
        if (jumping)
        {
            sinceJump = 0;
            body.velocity = new Vector2(body.velocity.x, jumpHeight);
            jumpTime += Time.deltaTime;
        }
        if (Input.GetKeyUp(KeyCode.Space) | jumpTime > buttonTime)
        {
            jumping = false;
        }

        //shooting code
        if (Input.GetKeyDown(KeyCode.W) && shotCooldown < sinceShot)
        {
            sinceShot = 0;
            GameObject p1 = Instantiate(projectile, this.transform.position, Quaternion.identity);
            GameObject p2 = Instantiate(projectile, this.transform.position, Quaternion.identity);

            p1.GetComponent<Rigidbody2D>().velocity = new Vector2(10, projectileSpeed);
            p2.GetComponent<Rigidbody2D>().velocity = new Vector2(projectileSpeed, 0);
        }

        //healthbar
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TakeDamage(1);
        }
    }

    void TakeDamage(int damage)
    {
        currentHealth -= damage;

        healthBar.SetHealth(currentHealth);
    }

    private void FixedUpdate()
    {
        //player movement
        this.transform.position = new Vector2(this.transform.position.x + horizontal/5, this.transform.position.y);
        //camera movement
        camera.transform.position = new Vector3(this.transform.position.x + CameraOffset, camera.transform.position.y, camera.transform.position.z);

    }

}



