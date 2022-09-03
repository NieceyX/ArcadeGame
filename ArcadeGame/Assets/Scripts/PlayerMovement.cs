using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D body;

    float horizontal;

    public float buttonTime = 0.5f;
    public float jumpHeight = 5;
    public float cancelRate = 100;
    float jumpTime;
    bool jumping;
    bool jumpCancelled;

    public float runSpeed = 1.0f;

    public Camera camera;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal") + 1.1f;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            float jumpForce = Mathf.Sqrt(jumpHeight * -2 * (Physics2D.gravity.y * body.gravityScale));
            body.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            jumping = true;
            jumpCancelled = false;
            jumpTime = 0;
        }
        if (jumping)
        {
            jumpTime += Time.deltaTime;
            if (Input.GetKeyUp(KeyCode.Space))
            {
                jumpCancelled = true;
            }
            if (jumpTime > buttonTime)
            {
                jumping = false;
            }
        }
    }
    private void FixedUpdate()
    {
        this.transform.position = new Vector2(this.transform.position.x + horizontal/5, this.transform.position.y);
        camera.transform.position = new Vector3(this.transform.position.x, camera.transform.position.y, camera.transform.position.z);
        if (jumpCancelled && jumping && body.velocity.y > 0)
        {
            body.AddForce(Vector2.down * cancelRate);
        }
    }

}



