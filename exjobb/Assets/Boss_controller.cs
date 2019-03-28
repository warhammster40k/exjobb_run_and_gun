using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_controller : MonoBehaviour
{
    public GameObject[] apples = new GameObject[0];
    public GameObject[] carrots = new GameObject[0];
    public GameObject spawn_Apples;
    public GameObject spawn_Carrots;

    public float AppleCooldown = 2f;
    public float CarrotCooldown = 2f;

    public int lifes = 50; 

    private float currAppleCooldown;
    private float currCarrotCooldown;

    // Start is called before the first frame update
    void Start()
    {
        currAppleCooldown = AppleCooldown;
        currCarrotCooldown = CarrotCooldown;
    }

    // Update is called once per frame
    void Update()
    {
        shootCarrots();
        shootApples(); 
    }

    void shootCarrots()
    {
        currCarrotCooldown -= Time.deltaTime;

        if(currCarrotCooldown < 0)
        {
            Random random = new Random();

            int rand = Random.Range(0, carrots.Length);

            Instantiate(carrots[rand]);

            currCarrotCooldown = CarrotCooldown;
        }
    }

    void shootApples()
    {
        currAppleCooldown -= Time.deltaTime;

        if (currAppleCooldown < 0)
        {
            Random random = new Random();

            int rand = Random.Range(0, apples.Length);

            Instantiate(apples[rand]);

            currAppleCooldown = AppleCooldown;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "projectile")
        {
            lifes--;

            if (lifes <= 0)
                Destroy(gameObject);
        }
    }
}
