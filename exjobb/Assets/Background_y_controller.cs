using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background_y_controller : MonoBehaviour
{
    private GameObject player; 

    public float speed = 1f;

    private float startTime;
    private float journeyLength;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");

        startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 destination = new Vector2(transform.position.x, player.transform.position.y);

        Vector2 StartPoss = transform.position;


        journeyLength = Vector2.Distance(StartPoss, destination);

        float distCovered = (Time.deltaTime - startTime) * speed;

        // Fraction of journey completed = current distance divided by total distance.
        float fracJourney = distCovered / journeyLength;

        // Set our position as a fraction of the distance between the markers.
        transform.position = Vector2.Lerp(StartPoss, destination, fracJourney);
    }
}
