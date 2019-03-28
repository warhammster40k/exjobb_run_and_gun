

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class background_scrolling : MonoBehaviour
{
    public bool followInY = false;

    public GameObject player;
    public float offset;
    private float changePosistion;
    public int nmrOfBackgrounds;

    // Background scroll speed can be set in Inspector with slider
    public float scrollSpeed = 0.001f;

    // Scroll offset value to smoothly repeat backgrounds movement
    public float scrollOffset;

    // Start position of background movement
    private Vector2 midPoss;


    // Backgrounds new position
    float newPos;

    // Use this for initialization
    void Start()
    {
        int offset = nmrOfBackgrounds / 2; // ser hur många bakgrounder och genom två för att hitta mitten

        midPoss = new Vector2(transform.position.x - (scrollOffset/ offset), transform.position.y);
           // Getting backgrounds start position
           //midPoss = transform.position;
    }

    // Update is called once per frame
    void Update()
    {

        midPoss = new Vector2(transform.position.x - (scrollOffset/2), transform.position.y);

        changePosistion = midPoss.x + offset + 6;

        Vector2 tempVelo = player.GetComponent<PlayerController>().getVelo();

        if (tempVelo.x < 0)
        {
            transform.position = new Vector2(transform.position.x - scrollSpeed, transform.position.y);
        }
        else if (tempVelo.x > 0)
        {
            transform.position = new Vector2(transform.position.x + scrollSpeed, transform.position.y);
        }

        loopBackground();
    }

    void loopBackground()
    {
   

        if (player.transform.position.x + offset > changePosistion)
        {
            transform.position = new Vector2(transform.position.x + scrollOffset, transform.position.y);
        }
        else if (player.transform.position.x - offset < (midPoss.x - offset) - 6)
        {
            //Debug.Log("hej");

            transform.position = new Vector2(transform.position.x - scrollOffset, transform.position.y);
        }
    }

    void yParalax()
    {
        float camerTopPoss = Camera.main.ScreenToWorldPoint(Vector3.zero).x;

        if (followInY && transform.position.x > camerTopPoss)
        {

        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1, 0, 0, 0.5f);


        Gizmos.DrawSphere(new Vector2(midPoss.x - offset, transform.position.y), 0.1f);
        Gizmos.DrawSphere(new Vector2(midPoss.x + offset, transform.position.y), 0.1f);

        Gizmos.DrawSphere(midPoss, 0.1f);

        Gizmos.DrawLine(player.transform.position, new Vector2 (player.transform.position.x - offset, player.transform.position.y));

  
        
    }
}
