using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class carrot_boss_controller : MonoBehaviour
{

    public float speed = 120;

    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        rb.AddForce(Vector2.left * speed);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {


        if (collision.gameObject.tag != ("Enemy") && collision.gameObject.tag != "projectile" && collision.gameObject.tag != "Star")
        {
            rb.bodyType = RigidbodyType2D.Static;

            Destroy(gameObject);
        }
    }

}
