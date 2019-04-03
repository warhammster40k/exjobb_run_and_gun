using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class star_controller : MonoBehaviour
{
    private star_system_controller stc;
    public bool taken = false;
    public bool found = false;

    public float findRadius = 4;

    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {

        player = GameObject.Find("Player");
        stc = GetComponentInParent<star_system_controller>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(player.transform.position, transform.position) < findRadius)
        {
            found = true;
            stc.StarTaken();
        } 
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

    public bool getFound()
    {
        return found;
    }
}
