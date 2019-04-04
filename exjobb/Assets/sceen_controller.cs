using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class sceen_controller : MonoBehaviour
{


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void reloadCurrScene()
    {

    }

    public void loadLevel(string lvName)
    {
        

        SceneManager.LoadScene(lvName);

        Time.timeScale = 1;
    }
}
