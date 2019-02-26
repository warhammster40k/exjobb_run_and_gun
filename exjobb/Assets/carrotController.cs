using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class carrotController : MonoBehaviour
{
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        

        if(collision.gameObject.tag != ("Enemy"))
        {
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
