using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class feedback_controller : MonoBehaviour
{
    public GameObject startPoss;
    public GameObject endPoss;
    public GameObject player;
    public GameObject starSystem;
    public GameObject canvas;
    public GameObject feedback_generell;
    public GameObject feedback_Sekundary;


    private float levelLength;
    private float ProcentInleven;

    [TextArea(3,10)]
    public string[] generallFeedback;

    [TextArea(3, 10)]
    public string[] secounddaryFeedback_allStarts;

    [TextArea(3, 10)]
    public string[] secounddaryFeedback_missedStars;

    [TextArea(3, 10)]
    public string[] secounddaryFeedback_noStars;

    // Olika listor med medelanden som slumpas ut beroende på vilket state som är aktivt 


    void Start()
    {
        levelLength = endPoss.transform.position.x - startPoss.transform.position.x;       
    }

    // Update is called once per frame
    void Update()
    {
        ProcentInleven =  (player.transform.position.x - startPoss.transform.position.x) / levelLength;

        if (ProcentInleven < 0) // ser till att procenten inte kan vara större eller mindre än 0 - 100%w
            ProcentInleven = 0;
        else if (ProcentInleven > 1)
            ProcentInleven = 1;

        if (Input.GetKeyDown(KeyCode.H))
        {
            Time.timeScale = 0;
            displayFeedback();

            
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            Time.timeScale = 1;

            canvas.SetActive(false);
        }
    }

    public void displayFeedback()
    {

        Time.timeScale = 0;

        int rand;
        Random random = new Random();

        canvas.SetActive(true);


        rand = Random.Range(0, generallFeedback.Length); //generella feedbacken

        feedback_generell.GetComponent<TextMeshProUGUI>().text = generallFeedback[rand];



        if (starSystem.GetComponent<star_system_controller>().getNoStartTaken() == true) // kollar om spelaren inte hittat någon stjärna 
        {
            rand = Random.Range(0, secounddaryFeedback_noStars.Length);

            feedback_Sekundary.GetComponent<TextMeshProUGUI>().text = secounddaryFeedback_noStars[rand];
        }        
        else if (starSystem.GetComponent<star_system_controller>().getMissedStar() == true)
        {
            rand = Random.Range(0, secounddaryFeedback_missedStars.Length); // sekundära feedbacken med alla stjärnor 

            feedback_Sekundary.GetComponent<TextMeshProUGUI>().text = secounddaryFeedback_missedStars[rand];
        }
        else
        {
            rand = Random.Range(0, secounddaryFeedback_allStarts.Length); // sekundära feedbacken med alla stjärnor 

            feedback_Sekundary.GetComponent<TextMeshProUGUI>().text = secounddaryFeedback_allStarts[rand];
        }
    }
    public float getProcentInleven()
    {
        return ProcentInleven;
    }
}
