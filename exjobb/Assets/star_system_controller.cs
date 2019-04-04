using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class star_system_controller : MonoBehaviour
{
    public GameObject[] starUi = new GameObject[0];
    public GameObject[] starslist = new GameObject[0];
    //public Sprite[] starSpriteList = new Sprite[0];

    private SpriteRenderer sr;
    private bool missedStar = false;
    private bool noStartTaken = true;

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StarTaken()
    {
        //int temp = 0;

        /*foreach(GameObject stars in starslist)
        {
            temp++;

            if (stars.GetComponent<star_controller>().getTaken())
            {
                
                Debug.Log("Star " + temp + "taken");
            }
        }*/

        for(int i = 0; i < starslist.Length; i++)
        {
            if(starslist[i].GetComponent<star_controller>().getTaken())
            {
                noStartTaken = false;

                starUi[i].gameObject.SetActive(true);

                if (i != 0)
                {
                    if (starslist[i - 1].GetComponent<star_controller>().getTaken()) //kollar om förra stjärnan är tagen, så finns det någon stjärna som är tagen men har en bakom som inte är tagen så kommer medelanden om missade stjärnoir presenteras 
                    {
                        missedStar = false;
                    }
                    else
                    {
                        missedStar = true;
                    }
                }
            }

            if (starslist[i].GetComponent<star_controller>().getFound() == true && starslist[i].GetComponent<star_controller>().getTaken() == false)
            {
                //kollar om man har hittat stjärnan men att den inte är tagen 
               // Debug.Log(i);
               // Debug.Log("Hej");


                if (i != 0)
                {
                    if (starslist[i - 1].GetComponent<star_controller>().getTaken())
                    {
                        missedStar = false;
                    }
                    else
                    {

                        missedStar = true;
                    }
                }
            }
        }
    }

    public bool getMissedStar()
    {
        return missedStar;
    }

    public bool getNoStartTaken()
    {
        return noStartTaken;
    }
}
