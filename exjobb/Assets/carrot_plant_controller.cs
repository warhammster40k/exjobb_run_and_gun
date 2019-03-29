using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class carrot_plant_controller : MonoBehaviour
{


    public float speed = 120;

    // Start is called before the first frame update
    void Start()
    {
        if (GetComponent<SpriteRenderer>().flipX == false)
            GetComponent<Rigidbody2D>().AddForce(Vector2.left * speed);
        if (GetComponent<SpriteRenderer>().flipX == true)
            GetComponent<Rigidbody2D>().AddForce(Vector2.right * speed);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {


        if (collision.gameObject.tag != ("Enemy") && collision.gameObject.tag != "projectile" && collision.gameObject.tag != "Star")
        {
            GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;

            Destroy(gameObject);
        }
    }
}
