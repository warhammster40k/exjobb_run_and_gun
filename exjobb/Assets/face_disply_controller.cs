using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class face_disply_controller : MonoBehaviour
{
    public GameObject startPoss;
    public GameObject endPoss;
    public GameObject feedbacksystem;
    public GameObject canvas;

    private feedback_controller Fc;

    private float length;

    // Start is called before the first frame update
    void Start()
    {
        Fc = feedbacksystem.GetComponent<feedback_controller>();

        length = endPoss.transform.position.x - startPoss.transform.position.x;

        canvas.SetActive(false);
    }

    private void Update()
    {
        displayFace();
    }

    void displayFace()
    {
        float newXPoss;

        newXPoss = length * Fc.getProcentInleven();

        transform.position = new Vector2(startPoss.transform.position.x + newXPoss, transform.position.y);
    }
}
