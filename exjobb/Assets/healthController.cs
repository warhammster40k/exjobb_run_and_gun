using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class healthController : MonoBehaviour
{
    public GameObject player;
    public Sprite[] sprites;

    private Image image;
    private PlayerController playerController;
    private int life;


    // Start is called before the first frame update
    void Start()
    {
        playerController = player.GetComponent<PlayerController>();
        image = GetComponent<Image>();
        life = playerController.life;
    }

    // Update is called once per frame
    void Update()
    {
        life = playerController.life;
        //image.sprite = sprites[0];

        if (life < 1)
            image.sprite = sprites[0];
        else if (life == 1)
            image.sprite = sprites[1];
        else if (life == 2)
            image.sprite = sprites[2];
        else if (life == 3)
            image.sprite = sprites[3];
        else
            image.sprite = sprites[3];
    }
}
