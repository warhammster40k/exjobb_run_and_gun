﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class slug_controller : MonoBehaviour
{

    private Rigidbody2D rb;
    private SpriteRenderer Sr;
    private Animator animator;
    public GameObject right;
    public GameObject left;

    public float speed; 

    private float dirr = -1;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Sr = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        flip();
        rb.velocity = Vector2.right * speed * dirr;
        Animation();

       
    }

    void Animation()
    {
        if (dirr < 0)
            Sr.flipX = false;
        else if (dirr > 0)
            Sr.flipX = true;
    }

    void flip()
    {
        if (transform.position.x < left.transform.position.x || transform.position.x > right.transform.position.x)
        {
            //Debug.Log("Flipping");
            dirr *= -1;
            rb.velocity = Vector2.zero;
        }
    }
}
