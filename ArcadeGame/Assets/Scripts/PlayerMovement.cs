using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    //health bar
    [Header("Health Bar")]
    public int maxHealth = 3;
    public int currentHealth;

    public HealthBar healthBar;

    //fuel bar
    [Header("Fuel Bar")]
    public int maxFuel = 5;
    public int currentFuel;
    public FuelBar fuelBar;

    //player settings
    [Header("Player Speed")]
    public float flightHeight = 10f;
    Rigidbody2D body;
    float horizontal;
    public float speedOffset = 6f;
    public float runSpeed = 1.1f;
    Vector2 originalPlace;

    //jump parameters
    [Header("Jump Settings")]
    public float buttonTime = 0.5f;
    public float jumpHeight = 5;
    float jumpTime = 100f;
    float sinceJump = 100;
    public float jumpCooldown = 3f;
    bool jumping;
    bool flying = false;

    //camera parameters
    [Header("Camera")]
    public Camera camera;
    public int CameraOffset = 5;

    //projectiles
    [Header("Player Projectiles")]
    public GameObject projectile;
    public float projectileSpeed = 20f;
    float projectileSpeedVert;
    public float shotCooldown = 3f;
    float sinceShot = 100;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        projectileSpeedVert = projectileSpeed;
        originalPlace = this.transform.position;

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
        if (Input.GetKeyDown(KeyCode.Space) && jumpCooldown < sinceJump && !flying)
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

            p1.GetComponent<Rigidbody2D>().velocity = new Vector2(10, projectileSpeedVert);
            p2.GetComponent<Rigidbody2D>().velocity = new Vector2(projectileSpeed, 0);
        }
        /*
        //healthbar
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TakeDamage(1);
        }*/
    }

    public void TakeDamage(int damage)
    {
        if (currentHealth <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);

        this.transform.position = originalPlace;
    }

    private void FixedUpdate()
    {
        //player movement
        this.transform.position = new Vector2(this.transform.position.x + horizontal/speedOffset, this.transform.position.y);
        //camera movement
        camera.transform.position = new Vector3(this.transform.position.x + CameraOffset, camera.transform.position.y, camera.transform.position.z);

    }

    //change to flying variables
    public void Switch()
    {
        projectileSpeedVert = -projectileSpeed;
        flying = true;
        body.gravityScale = 0;
        this.transform.position = new Vector2(this.transform.position.x, this.transform.position.y + flightHeight);
        originalPlace = this.transform.position;
    }

}



