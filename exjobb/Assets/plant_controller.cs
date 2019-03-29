using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class plant_controller : MonoBehaviour
{
    private bool attacking = false;
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private Animator animator;


    public GameObject Carrot;
    private GameObject TempCarrot;

    public float projectileSpeed = 120f;
    public float attackSpeed = 2F;
    private float currAttackSpeed;



    // Start is called before the first frame update
    void Start()
    {
        TempCarrot = Carrot; 

        currAttackSpeed = attackSpeed;

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

        if (sr.flipX == false)
            Instantiate(TempCarrot, new Vector2(transform.position.x - 1, transform.position.y), transform.rotation);
        if (sr.flipX == true)
            Instantiate(TempCarrot, new Vector2(transform.position.x + 1, transform.position.y), transform.rotation);

        attacking = false;
    }

    void kill()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == ("projectile"))
        {

            animator.SetBool("Kill", true);
            StartCoroutine(Destroy());
        }
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
