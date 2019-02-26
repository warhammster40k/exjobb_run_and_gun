using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : PhysicsObject
{
    bool crouching; 

    public float jumpStartForce = 7f;
    public float maxSpeed = 7f;

    public float knockbackForce = 2f;

    public float bulletSpeed = 50;

    public GameObject acorn;


    // aim vectors 
    private Vector3 AimRight = Vector2.right;
    private Vector3 AimLeft = Vector2.left;
    private Vector3 AimUp = Vector2.up;
    private Vector3 AimRightUp = new Vector2(0.5f, 0.5f);
    private Vector3 AimLeftUp = new Vector2(-0.5f, 0.5f);
    private Vector3 aimAngle;


    private Animator animator;
    private SpriteRenderer spriteRenderer;

    public int life = 3;

    void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    protected override void ComputeVelo()
    {
        Vector2 move = Vector2.zero;

        move.x = Input.GetAxis("Horizontal");

        if (Input.GetButtonDown("Jump") && grounded)
        {
            velo.y = jumpStartForce;
        }
        else if (Input.GetButtonUp("Jump"))
        {
            if(velo.y > 0)
            {
                velo.y = velo.y * 0.5f;
            }
        }

        if (Input.GetButton("Vertical"))
            crouching = true; 
        else if (Input.GetButtonUp("Vertical"))
            crouching = false;

        targetVelo = move * maxSpeed;

        if(targetVelo.x > 0)
        {
            spriteRenderer.flipX = false;
        }
        else if (targetVelo.x < 0)
        {
            spriteRenderer.flipX = true;
        }
        
        if(crouching)
        {
            targetVelo = Vector2.zero;
        }

        aim();
        shoot();
        animations();

    }

    private void aim()
    {
        Vector3 p = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        aimAngle = p - transform.position ;
        //Debug.Log("Mouse angle  " + Vector2.Angle(aimAngle, transform.position));
    }

    private void shoot()
    {
        

        if (Input.GetMouseButtonDown(0))
        {
            GameObject Clone;

            aimAngle = aimAngle.normalized;
            Clone = (Instantiate(acorn, transform.position + aimAngle.normalized, transform.rotation)) as GameObject;

            Clone.GetComponent<Rigidbody2D>().velocity = new Vector2 (aimAngle.x, aimAngle.y).normalized * bulletSpeed;
        }  
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == ("Enemy"))
        {
            life--;
            rb.AddForce(new Vector2(-velo.x, velo.y) * knockbackForce);
        }
    }

    void OnDrawGizmosSelected()
    {
        // Draw a semitransparent blue cube at the transforms position
        Gizmos.color = new Color(1, 0, 0, 0.5f);

        Vector3 p = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float angel = Vector3.Angle(transform.position.normalized, p);

        Gizmos.DrawLine(transform.position, transform.position + AimUp);
        Gizmos.DrawLine(transform.position, transform.position + AimRight);
        Gizmos.DrawLine(transform.position, transform.position + AimLeftUp);
        Gizmos.DrawLine(transform.position, transform.position + AimLeft);
        Gizmos.DrawLine(transform.position, transform.position + AimRightUp);

        Gizmos.DrawLine(transform.position, p);

        Gizmos.DrawSphere(p, 0.1F);
    }

    private void animations()
    {
        if(targetVelo.x != 0) // running
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

    }
}
