using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class basket_controller : MonoBehaviour
{
    public GameObject carrot;
    public float attackCooldown = 0.6f;
    private float currAttackCooldown; 

    // Start is called before the first frame update
    void Start()
    {
        currAttackCooldown = attackCooldown;
    }

    // Update is called once per frame
    void Update()
    {
        shoot();
    }

    void shoot()
    {
        currAttackCooldown -= Time.deltaTime;

        if(currAttackCooldown < 0)
        {
            Instantiate(carrot, transform.position, transform.rotation);

            currAttackCooldown = attackCooldown; 
        }
    }
}
