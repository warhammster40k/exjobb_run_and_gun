using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet_controller : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator ani;
    public bool kill = false;

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
            killing();
        }

        if (collision.gameObject.tag == ("Enemy"))
        {
            Destroy(gameObject);
        }
    }


    public void killing()
    {
        ani.SetBool("Kill", true);
        StartCoroutine(Destroy());
    }

    private IEnumerator Destroy()
    {

        yield return new WaitForSeconds(0.25f);
        Destroy(gameObject);
    }
}
