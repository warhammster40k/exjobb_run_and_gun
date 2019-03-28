using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class star_controller : MonoBehaviour
{
    private star_system_controller stc;
    private bool taken = false;

    // Start is called before the first frame update
    void Start()
    {
        stc = GetComponentInParent<star_system_controller>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            taken = true;
            stc.StarTaken();

            GetComponent<BoxCollider2D>().enabled = false;
            GetComponent<SpriteRenderer>().enabled = false;
        }
    }

    public bool getTaken()
    {
        return taken;
    }
}
