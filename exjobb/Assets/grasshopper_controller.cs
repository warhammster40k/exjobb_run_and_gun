using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class grasshopper_controller : MonoBehaviour

{

    private Rigidbody2D rb;
    private SpriteRenderer Sr;

    public float JumpCooldown = 1f;
    public float jumpForce = 2;
    public int life = 1; 

    public bool isAirbound = false;

    private int dirr = 1;

    private float currenJumpCooldown;
    private Vector2 startPoss;

    private float checkLenght = 3f;
    public float bounderyLenght = 20f;
    private Animator animator;

    private float leftBond;
    private float rightBond;

    private Vector2 lastPoss;
    private int fallingOrJumping;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Sr = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();

        startPoss = transform.position;

        rightBond = startPoss.x + bounderyLenght;
        leftBond = startPoss.x - bounderyLenght;
    }

    // Update is called once per frame
    void Update()
    {
        changeDirr();
        jump();
        Animation();

        /*if (lastPoss.y < transform.position.y)
            animator.SetFloat("Speed_jump", -1);
        else if (lastPoss.y > transform.position.y)
            animator.SetFloat("Speed_jump", 1);
        else if (isAirbound == false)
            animator.SetBool("Grounded", true);
            */

        if (isAirbound == true)
            animator.SetBool("Grounded", false);
        else if (isAirbound == false)
            animator.SetBool("Grounded", true);

        lastPoss = transform.position;

    }
    
    void jump()
    {
        if (currenJumpCooldown <= 0f)
        {
            rb.AddForce(new Vector2(0.5f * dirr, 0.7f).normalized * jumpForce);
            currenJumpCooldown = JumpCooldown;

            isAirbound = true;
        }

        currenJumpCooldown -= Time.deltaTime;
    }

    void changeDirr()
    {
        if (transform.position.x + checkLenght > rightBond && dirr == 1)
        {
            dirr *= -1;
        }
        else if (transform.position.x - checkLenght < leftBond && dirr == -1)
        {
            dirr *= -1;
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "wall")
        {
            rb.velocity = Vector2.zero;
            isAirbound = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == ("projectile"))
        {
            rb.bodyType = RigidbodyType2D.Static;

            animator.SetBool("Kill", true);
            StartCoroutine(Destroy());
        }
    }

    private IEnumerator Destroy()
    {

        yield return new WaitForSeconds(0.33f);
        Destroy(gameObject);
    }

    void Animation()
    {
        if (isAirbound == false) // cahgne dirr of sprite
        {
            if (dirr > 0)
            {
                Sr.flipX = true;
            }
            else if (dirr < 0)
            {
                Sr.flipX = false;
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        // Draw a semitransparent blue cube at the transforms position
        Gizmos.color = new Color(1, 0, 0, 0.5f);

        Gizmos.DrawLine(transform.position, new Vector2 (transform.position.x + checkLenght, transform.position.y));

        Gizmos.DrawSphere(new Vector2(startPoss.x + bounderyLenght, transform.position.y), 0.1F);
        Gizmos.DrawSphere(new Vector2(startPoss.x - bounderyLenght, transform.position.y), 0.1F);

    }

}
