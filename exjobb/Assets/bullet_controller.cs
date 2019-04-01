using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet_controller : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator ani;
    private bool kill = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        ani = GetComponent<Animator>();

       // rb.velocity = transform.right * 20;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == ("wall"))
        {
            rb.velocity = Vector2.zero;
            rb.gravityScale = 0;
            killing();
        }

        if (collision.gameObject.tag == ("Enemy"))
        {
            if (collision.gameObject.name == "slug")
            {
               

                rb.velocity = Vector2.zero;
                rb.gravityScale = 2.5f;

                if (transform.position.x < collision.transform.position.x) 
                    rb.AddForce(new Vector2(-0.5f, 0.7f) * 0.04f);
                else
                    rb.AddForce(new Vector2(0.5f, 0.7f)* 0.04f);
            }
            else
            {
                //Debug.Log("Hej");
                Destroy(gameObject);
            }
        }
    }


    public void killing()
    {
        ani.SetBool("Kill", true);
        StartCoroutine(Destroy());
    }

    private IEnumerator Destroy()
    {
        //Debug.Log("Hej");
        yield return new WaitForSeconds(0.33f);
        Destroy(gameObject);
    }
}
