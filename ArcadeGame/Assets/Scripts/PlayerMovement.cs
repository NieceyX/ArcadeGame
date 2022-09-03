using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D body;

    float horizontal;

    public float buttonTime = 0.5f;
    public float jumpHeight = 5;
    float jumpTime = 100f;
    float sinceJump = 0;
    public float jumpCooldown = 3f;
    bool jumping;

    public float runSpeed = 1.1f;

    public Camera camera;
    public int CameraOffset = 5;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        sinceJump += Time.deltaTime;

        horizontal = Input.GetAxisRaw("Horizontal") + runSpeed;

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
    }
    private void FixedUpdate()
    {
        this.transform.position = new Vector2(this.transform.position.x + horizontal/5, this.transform.position.y);
        camera.transform.position = new Vector3(this.transform.position.x + CameraOffset, camera.transform.position.y, camera.transform.position.z);
       /* if (jumpCancelled && jumping && body.velocity.y > 0)
        {
            body.AddForce(Vector2.down * cancelRate);
        }*/
    }

}



