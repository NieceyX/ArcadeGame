using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public Animator truckAnimator;
    public Animator UFOAnimator;
    public GameObject truckImage;
    public GameObject UFOImage;


    //health bar
    [Header("Health Bar")]
    public int maxHealth = 3;
    public int currentHealth;

    public HealthBar healthBar;

    //fuel bar
    [Header("Fuel Bar")]
    public int maxFuel = 5;
    public float currentFuel;
    float coef = 0.2f;

    public FuelBar fuelBar;

    //player settings
    [Header("Player Speed")]
    public float flightHeight = 10f;
    Rigidbody2D body;
    float horizontal;
    float vertical;
    public float speedOffset = 6f;
    public float runSpeed = 1.1f;
    Vector2 originalPlace;
    bool checkpoint = false;

    public bool end = false;

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
    public float shotDestroy = 2f;
    float sinceShot = 100;
    GameObject p1;
    GameObject p2;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        projectileSpeedVert = projectileSpeed;
        originalPlace = this.transform.position;
        vertical = 0;

        //healthbar
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);

        //fuelbar
        currentFuel = maxFuel;
        fuelBar.SetMaxFuel(maxFuel);

        Time.timeScale = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //timers
        sinceJump += Time.deltaTime;
        sinceShot += Time.deltaTime;

        if (Time.timeScale != 0)
        {
            FuelDeplete(coef * Time.deltaTime);
        }


        //check for left/right input
        horizontal = Input.GetAxisRaw("Horizontal") + runSpeed;

        if (flying)
        {
            vertical = Input.GetAxisRaw("Vertical");
        }

        //jumping code -- jump slightly longer/higher for a longer press
        if (Input.GetKeyDown(KeyCode.W) && jumpCooldown < sinceJump && !flying)
        {
            sinceJump = 0;
            jumping = true;
            jumpTime = 0;
            truckAnimator.SetBool("isJump", true);
        }
        if (jumping)
        {
            sinceJump = 0;
            body.velocity = new Vector2(body.velocity.x, jumpHeight);
            jumpTime += Time.deltaTime;
            truckAnimator.SetFloat("yVelocity", body.velocity.y);
            Debug.Log("yVelocity" + transform.position.y);
        }
        if (Input.GetKeyUp(KeyCode.W) || jumpTime > buttonTime)
        {
            jumping = false;
            truckAnimator.SetBool("isJump", false);
        }

        //shooting code
        if (sinceShot >= shotDestroy)
        {
            Destroy(p1);
            Destroy(p2);
        }
        if (Input.GetKeyDown(KeyCode.Space) && shotCooldown < sinceShot && Time.timeScale != 0)
        {
            sinceShot = 0;
            p1 = Instantiate(projectile, this.transform.position, Quaternion.identity);
            p2 = Instantiate(projectile, this.transform.position, Quaternion.identity);

            p1.GetComponent<Rigidbody2D>().velocity = new Vector2(10, projectileSpeedVert);
            p2.GetComponent<Rigidbody2D>().velocity = new Vector2(projectileSpeed, 0);

        }

        if (Input.GetKeyDown(KeyCode.R) && currentHealth <= 0)
        {
            Time.timeScale = 1;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        if (Input.GetKeyDown(KeyCode.R) && end)
        {
            Time.timeScale = 1;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        if (Input.GetKeyDown(KeyCode.R) && Time.timeScale == 0)
        {
            GameObject canvas = GameObject.Find("Canvas");
            canvas.transform.Find("StartGame").gameObject.SetActive(false);
            Time.timeScale = 1;
        }
        
    }


    

    public void FuelDeplete(float damage)
    {
        if (currentFuel <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        currentFuel -= damage;
        fuelBar.SetFuel(currentFuel);
    }

    public void TakeDamage(int damage)
    {
        if (currentHealth <= 0)
        {
            Time.timeScale = 0;
            GameObject canvas = GameObject.Find("Canvas");
            canvas.transform.Find("GameOver").gameObject.SetActive(true);
        }

        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);

        this.transform.position = originalPlace;
        currentFuel = maxFuel;
        fuelBar.SetFuel(currentFuel);

        if (checkpoint == true)
        {
            GameObject.Find("Checkpoint").GetComponent<CheckPoint>().Trigger();
        }
    }

    private void FixedUpdate()
    {
        //player movement
        this.transform.position = new Vector2(this.transform.position.x + horizontal/speedOffset, this.transform.position.y + vertical/speedOffset);
        //camera movement
        camera.transform.position = new Vector3(this.transform.position.x + CameraOffset, camera.transform.position.y, camera.transform.position.z);

    }


    public void Truck()
    {
        if (flying)
        {
            projectileSpeedVert = -projectileSpeed;
            flying = false;
            body.gravityScale = 5;
            this.transform.position = new Vector2(this.transform.position.x + 2, 0);
            this.transform.Find("Truck").gameObject.SetActive(true);
            this.transform.Find("UFO").gameObject.SetActive(false);
            coef = .2f;
        }
        originalPlace = this.transform.position;
        checkpoint = true;
    }

    //change to flying variables
    public void Plane()
    {
        if (!flying)
        {
            projectileSpeedVert = -projectileSpeed;
            flying = true;
            body.gravityScale = 0;
            coef = .4f;
            this.transform.position = new Vector2(this.transform.position.x + 2, flightHeight);
            this.transform.Find("Truck").gameObject.SetActive(false);
            this.transform.Find("UFO").gameObject.SetActive(true);
        }
        originalPlace = this.transform.position;
        checkpoint = true;
    }

    public void AddFuel(int _value)
    {
        currentFuel = Mathf.Clamp(currentFuel + _value, 0, maxFuel);
    }

    public void ChangeAvata(bool isTruck)
    {
        truckImage.SetActive(isTruck ? true : false);
        UFOImage.SetActive(isTruck ? false : true);
    }

}



