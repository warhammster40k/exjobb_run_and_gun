using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Point_controller : MonoBehaviour
{
    public int totalPoints;



    public TextMeshProUGUI pointText;

    private void Update()
    {
        pointText.text = totalPoints.ToString(); 
    }

    public void addPoints(int points)
    {
        totalPoints += points;
    }

    public int getPoints()
    {
        return totalPoints;
    }
}
