using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private bool crouching;
    private bool grounded = true;
    public bool canMove = true;
    public bool isTakingDamage = false;

    public float shootCooldown = 0.5f;
    private float currShootCooldown;

    private int numberOfStars = 0;
    public int numberOfJumpes;
    private int currNumJumps;

    public float jumpStartForce = 7f;
    public float maxSpeed = 7f;

    public float knockbackForce = 2f;

    public float bulletSpeed = 50;

    public GameObject acorn;

    public float damageTime;
    private float currDamageTime;

    //private float groundDistance = 0.1f;

    // aim vectors 
    private Vector3 AimRight = Vector2.right;
    private Vector3 AimLeft = Vector2.left;
    private Vector3 AimUp = Vector2.up;
    private Vector3 AimRightUp = new Vector2(0.5f, 0.5f);
    private Vector3 AimLeftUp = new Vector2(-0.5f, 0.5f);
    private Vector3 aimAngle;


    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;


    public int life = 3;

    void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();

        currNumJumps = numberOfJumpes;
        currShootCooldown = shootCooldown;
        currDamageTime = damageTime;
    }

    private void Update()
    {
        move();
        aim();
        shoot();
        damageCooldown();
        checkIfOnGround();
        animations();
    }

    private void move()
    {
        if (canMove)
        {
            Vector2 move = Vector2.zero;

            move.x = Input.GetAxis("Horizontal");

            if (currNumJumps > 0)
            {
                if (Input.GetButtonDown("Jump"))
                {
                    currNumJumps--;
                    rb.velocity = new Vector2(rb.velocity.x, 0);
                    rb.AddForce(new Vector2(0, jumpStartForce));

                }
                else if (Input.GetButtonUp("Jump"))
                {
                    if (rb.velocity.y > 0)
                    {
                        rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
                    }
                }
            }

            else if (Input.GetButtonUp("Jump"))
            {
                if (rb.velocity.y > 0)
                {
                    rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
                }
            }

            rb.velocity = new Vector2(move.x * maxSpeed, rb.velocity.y);

        }
    }

    private void aim()
    {
        Vector3 p = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        aimAngle = p - transform.position ;
        //Debug.Log("Mouse angle  " + Vector2.Angle(aimAngle, transform.position));
    }

    private void shoot()
    {
        currShootCooldown -= Time.deltaTime;

        if (Input.GetMouseButtonDown(0))
        {
            if (currShootCooldown < 0)
            {
                GameObject Clone;

                aimAngle = aimAngle.normalized;
                Clone = (Instantiate(acorn, transform.position + aimAngle.normalized * 0.1f, transform.rotation)) as GameObject;

                Clone.GetComponent<Rigidbody2D>().velocity = new Vector2(aimAngle.x, aimAngle.y).normalized * bulletSpeed;

                currShootCooldown = shootCooldown;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == ("Enemy"))
        {
            if (isTakingDamage == false)
            {
                canMove = false;
                isTakingDamage = true;

                life--;

                rb.velocity = Vector2.zero;

                if(transform.position.x < collision.transform.position.x) //kollar vilket håll man ska skjuta spelaren
                    rb.AddForce(new Vector2(-0.5f, 0.7f) * knockbackForce);
                else
                    rb.AddForce(new Vector2(0.5f, 0.7f) * knockbackForce);

                currDamageTime = damageTime;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Star")
        {
            numberOfStars++;
            //Destroy(collision.gameObject);
        }
        if (collision.gameObject.tag == ("Enemy"))
        {
            if (isTakingDamage == false)
            {
                canMove = false;
                isTakingDamage = true;

                life--;

                rb.velocity = Vector2.zero;

                if (transform.position.x < collision.transform.position.x) //kollar vilket håll man ska skjuta spelaren
                    rb.AddForce(new Vector2(-0.5f, 0.7f) * knockbackForce);
                else
                    rb.AddForce(new Vector2(0.5f, 0.7f) * knockbackForce);

                currDamageTime = damageTime;
            }
        }
    }
    public Vector2 getVelo()
    {
        return rb.velocity;
    }

    void checkIfOnGround()
    {

        float halfColider = GetComponent<BoxCollider2D>().size.x / 2 ;
        Vector3 halfColiderVectorR = new Vector2(transform.position.x + 0.235f, transform.position.y);
        Vector3 halfColiderVectorL = new Vector2(transform.position.x - 0.354f, transform.position.y);

        RaycastHit2D ray = Physics2D.Raycast(transform.position, -Vector2.up, 1f);
        RaycastHit2D ray1 = Physics2D.Raycast(halfColiderVectorR, -Vector2.up, 1f);
        RaycastHit2D ray2 = Physics2D.Raycast(halfColiderVectorL, -Vector2.up, 1f);

        Debug.DrawRay(transform.position, -Vector2.up, Color.green);
        Debug.DrawRay(halfColiderVectorR, -Vector2.up, Color.green);
        Debug.DrawRay(halfColiderVectorL, -Vector2.up, Color.green);

        if (ray.collider != null)
        {
            GameObject temp = ray.transform.gameObject;

            if (temp.tag == "wall")
            {
                grounded = true;
                currNumJumps = numberOfJumpes;
            }
        }
        else if (ray1.collider != null)
        {
            GameObject temp = ray1.transform.gameObject;

            if (temp.tag == "wall")
            {
                grounded = true;
                currNumJumps = numberOfJumpes;
            }
        }
        else if (ray2.collider != null)
        {
            GameObject temp = ray2.transform.gameObject;

            if (temp.tag == "wall")
            {
                grounded = true;
                currNumJumps = numberOfJumpes;
            }
        }
        else
            grounded = false;
    }

    private void damageCooldown()
    {
        if (isTakingDamage)
        {
            currDamageTime -= Time.deltaTime;

            if (currDamageTime < 0)
            {
                canMove = true;
                isTakingDamage = false;
                currDamageTime = damageTime;
            }
        }
    }

    private void animations()
    {
        if(rb.velocity.x != 0) // running
        {
            animator.SetBool("isRunning", true);
        }
        else
        {
            animator.SetBool("isRunning", false);
        }

        if(grounded == false)
        {
            animator.SetBool("isJumping", true);
        }
        else
        {
            animator.SetBool("isJumping", false);
        }

        if(crouching)
        {
            animator.SetBool("isCroushing", true);
        }
        else
        {
            animator.SetBool("isCroushing", false);
        }
        if(isTakingDamage)
        {
            animator.SetBool("isHurting", true);
        }
        else
        {
            animator.SetBool("isHurting", false);
        }
        if (rb.velocity.x > 0)
        {
            spriteRenderer.flipX = false;
        }
        else if (rb.velocity.x < 0)
        {
            spriteRenderer.flipX = true;
        }
    }
}
