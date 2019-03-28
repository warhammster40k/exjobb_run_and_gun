using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_trigger_controller : MonoBehaviour
{
    public Camera cam;
    public GameObject camPosition;
    private BoxCollider2D bc;
    public GameObject backGround;
    public GameObject oldBackGround;

    // Start is called before the first frame update
    void Start()
    {
            
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            cam.transform.position = camPosition.transform.position;


            cam.orthographicSize = 9.84f;
            cam.GetComponent<camraController>().enabled = false;

            oldBackGround.SetActive(false);
            backGround.SetActive(true); 

            Destroy(gameObject);
        }


    }
}
