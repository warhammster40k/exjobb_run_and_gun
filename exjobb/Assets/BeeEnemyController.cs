using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeEnemyController : MonoBehaviour
{
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private int activePatrollPoint = 0;
    private Animator animator;

    private Vector2 lastPoss;


    private float currLerpTime;
    private float lerpTime = 4f;
    private float currCooldown;

    
    public GameObject Carrot;
    public GameObject player; 
    public GameObject[] patrollPoints = new GameObject[0];
    private Vector2 currPossision;
    public float speed = 2f;
    public float agroRange = 2f;
    public float attackCooldown = 2f; 



    // Start is called before the first frame update
    void Start()
    {
        lerpTime = speed;
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        currPossision = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        animations();
        attack();
    }
    private void FixedUpdate()
    {
        move();
    }

    void move()
    {
        if (patrollPoints.Length > 0)
        {
           

            if (currLerpTime <= lerpTime)
            {
                currLerpTime += Time.deltaTime;
                //Debug.Log("currLerpTime  " + currLerpTime);


                transform.position = Vector2.Lerp(currPossision, patrollPoints[activePatrollPoint].transform.position, currLerpTime / lerpTime);

            }
            else
            {
                transform.position = patrollPoints[activePatrollPoint].transform.position;

                currLerpTime = 0;

                currPossision = transform.position;

                activePatrollPoint++;

                if (activePatrollPoint == patrollPoints.Length || activePatrollPoint > patrollPoints.Length)
                    activePatrollPoint = 0;
            }
        }

        //float distanceSqr = (patrollPoints[activePatrollPoint].transform.position - transform.position).sqrMagnitude; // storlek kopplad till hur nära man är en patrull point

    }

    void attack()
    {
        currCooldown += Time.deltaTime;

        if (currCooldown >= attackCooldown)
        {

            Instantiate(Carrot, transform.position, transform.rotation);

            currCooldown = 0;
        }
    }

    void animations()
    {
        if (transform.position.x > lastPoss.x)
            sr.flipX = true;
        else if (transform.position.x < lastPoss.x)
            sr.flipX = false;

        lastPoss = transform.position;
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
}
