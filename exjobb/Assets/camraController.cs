using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class camraController : MonoBehaviour
{
    public GameObject player;
    public float recSice = 3f;

    private Vector3 offset;

    private bool chasing;

    // Start is called before the first frame update
    void Start()
    {
        chasing = false;
        offset = new Vector3(0,0,-1f);
        transform.position = player.transform.position + offset;
    }
    void OnDrawGizmosSelected()
    {
        // Draw a semitransparent blue cube at the transforms position
        Gizmos.color = new Color(1, 0, 0, 0.5f);
        Gizmos.DrawCube(transform.position, new Vector3(3, 3, 3));
    }

    void LateUpdate()
    {
       

        float cameraRec = (transform.position - player.transform.position).sqrMagnitude;

        if (cameraRec > 3f)
        {
            chasing = true;
        }

        if (chasing)
        {

            Vector3 position = Vector2.Lerp((Vector2)transform.position, (Vector2)player.transform.position, Time.deltaTime * 3);
            transform.position = position + offset;


            float distanceSqr = (transform.position - player.transform.position).sqrMagnitude;

            

            if (distanceSqr < 1.1f)
            {
                chasing = false;
            }
        }
    }

    /*private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            chasing = true;
        }
    }*/

}
