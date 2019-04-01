using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class plant_controller : MonoBehaviour
{
    private bool attacking = false;
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private Animator animator;

    public int life = 3;

    public GameObject Carrot;
    private GameObject TempCarrot;

    public float projectileSpeed = 120f;
    public float attackSpeed = 2F;
    private float currAttackSpeed;

    public float startShootOffsett = 2f;


    // Start is called before the first frame update
    void Start()
    {
        TempCarrot = Carrot; 

        currAttackSpeed = startShootOffsett;

        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        if(sr.flipX == true)
            Carrot.GetComponent<SpriteRenderer>().flipX = true;
        else
            Carrot.GetComponent<SpriteRenderer>().flipX = false;

    }

    // Update is called once per frame
    void Update()
    {
        currAttackSpeed -= Time.deltaTime;

        if(currAttackSpeed < 0)
        {
            attacking = true;

            currAttackSpeed = attackSpeed;
        }

        Animation();
    }

    void attack()
    {

        GameObject Clone;

        if (sr.flipX == false)
        {
            Clone = Instantiate(TempCarrot, new Vector2(transform.position.x - 1, transform.position.y), transform.rotation);
            Clone.GetComponent<SpriteRenderer>().flipX = false;
        }
        if (sr.flipX == true)
        {
            Clone = Instantiate(TempCarrot, new Vector2(transform.position.x + 1, transform.position.y), transform.rotation);
            Clone.GetComponent<SpriteRenderer>().flipX = true;
        }

        attacking = false;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == ("projectile"))
        {
            takingDamage();
        }
    }

    private void takingDamage()
    {
        life--;

        sr.color = Color.red;



        StartCoroutine(cooldown());

        if (life <= 0)
        {
            sr.color = Color.white;
            animator.SetBool("Kill", true);
            GetComponent<BoxCollider2D>().enabled = false;
            StartCoroutine(Destroy());
        }
    }

    private IEnumerator cooldown()
    {

        yield return new WaitForSeconds(0.2f);

        sr.color = Color.white;
    }

    private IEnumerator Destroy()
    {

        yield return new WaitForSeconds(0.33f);
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }


    void Animation()
    {
        if (attacking)
            animator.SetBool("attacking", true);
        else
            animator.SetBool("attacking", false);
    }
}
