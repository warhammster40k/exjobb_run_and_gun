using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class carrotController : MonoBehaviour
{
    private Animator animator;
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        

        if(collision.gameObject.tag != ("Enemy") && collision.gameObject.tag != "projectile" )
        {
            rb.bodyType = RigidbodyType2D.Static;

            animator.SetBool("Hitting", true);
            StartCoroutine (Destroy());
        }
    }

    private IEnumerator Destroy()
    {

        yield return new WaitForSeconds(0.15f);
        Destroy(gameObject);
    }
}
