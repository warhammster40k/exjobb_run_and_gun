using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsObject : MonoBehaviour //baserad på en unity tutorial
{
    private float gravityModi = 1f;
    private float minGroundNormalY = 0.65f;
    private float chell = 0.01f; // lägger till ett "skal" runt colidern för att se till att den inte fastnar i andra coliders 

    protected Vector2 targetVelo;
    protected Rigidbody2D rb;
    protected float minMoveDis = 0.001f; // denna kollar så att inte koden kommer kolla colliision alla objekt som står still 
   
    protected Vector2 velo;
    protected Vector2 groundNormal;
    protected ContactFilter2D contactFilter;
    protected RaycastHit2D[] hitBuffer = new RaycastHit2D[16]; // hår många colissions som kan ske i en cast 
    protected List<RaycastHit2D> hitBufferlist = new List<RaycastHit2D>(16);

    protected bool grounded;

    private void OnEnable()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        contactFilter.useTriggers = false;
        contactFilter.SetLayerMask(Physics2D.GetLayerCollisionMask(gameObject.layer)); // använda layes matrixen för att se vilka colissionen som används 
        contactFilter.useLayerMask = true;
    }

    void Update()
    {
        //targetVelo = Vector2.zero;
        ComputeVelo();
    }

    protected virtual void ComputeVelo()
    {

    }

    private void FixedUpdate()
    {
        velo += gravityModi * Physics2D.gravity * Time.deltaTime; //modifierar unitys grund gravitation med modifiern 
        velo.x = targetVelo.x;

        grounded = false;

        Vector2 moveAlongGround = new Vector2(groundNormal.y, -groundNormal.x); // målar ut normalen som är 90% efter normalen man går på så på flat mark är den till höger elkler vänster men i slopes är den lika med dess lutning
        Vector2 deltaPosition = velo * Time.deltaTime;
        Vector2 move = moveAlongGround * deltaPosition.x;

        movement(move, false); // går i sidled

        move = Vector2.up * deltaPosition.y; // grativation i Y led 

        movement(move, true); // går i y led

    }

    void movement(Vector2 move, bool yMovement)
    {
        float distance = move.magnitude; //ger vectorn längd vilket då kommer göra att checken blir längre med att hastigheten ökar för att garantera att kolisionen fungerar 

        if (distance > minMoveDis)
        {
            int count = rb.Cast(move, contactFilter, hitBuffer, distance + chell); // cast - kastar ut colidern på rd2d där den kommer vara i nästa frame ("distance") och ser om någonting koliderar 
            hitBufferlist.Clear(); //inte använda gammal data
            for (int i = 0; i < count; i++)
            {
                hitBufferlist.Add(hitBuffer[i]); //hitbuffer är de saker som träffades, count är hur många som träffades  
            }

            for (int k = 0; k < hitBufferlist.Count; k++ )
            {
                Vector2 currentNormal = hitBufferlist[k].normal; //kollar om någonting är på marken (koliderat med någonting i y led då)

                if (currentNormal.y > minGroundNormalY) //är den colisionen vi koliderar med faktiskt mark eller är det en sida detta ses genomn att gämföra med minimun normalen
                //if(currentNormal.normalized.y >minGroundNormalY)
                {
                    //Debug.Log("currentNormal " + currentNormal.y);
                    
                    grounded = true;

                    if(yMovement)
                    {
                        groundNormal = currentNormal;
                        currentNormal.x = 0;
                    }
                }
                float projection = Vector2.Dot(velo, currentNormal); 

                if(projection < 0)
                {
                    velo = velo - projection * currentNormal; //Tar ut den velossitet som har stoppat rörelse men gör inte att karraktären stoppas hel i rörelse 
                }

                float ModiDistance = hitBufferlist[k].distance - chell;

                distance = ModiDistance < distance ? ModiDistance : distance;
            }
        }

        rb.position += move.normalized*distance;
    }
}
