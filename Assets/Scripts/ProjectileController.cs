using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{

    public float aliveTime;
    public bool destroyUponPlayer;
    public BoxCollider2D bulletCollider;
    public bool destroyOnImpact;

    void Start()
    {
        Destroy(gameObject, aliveTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
      
    
        if (collision.tag == "Player")
        {
            Debug.LogWarning("Player hit detected");

            if (destroyUponPlayer == true)
            {
                collision.GetComponent<Controller2D>().RestartGame();
                Destroy(gameObject);
            }
        }

        if (collision.tag == "Turret")
        {
            Debug.LogWarning("Turret Hit");
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {


        if (collision.tag != "Player" && collision.tag != "Turret")
        {

            Destroy(gameObject);
        }

        if (collision.tag == "Player")
        {
            Debug.LogWarning("Player hit detected");

            if (destroyUponPlayer == true)
            {
                collision.GetComponent<Controller2D>().RestartGame();
                Destroy(gameObject);
            }
        }

        if (collision.tag == "Turret")
        {
            Debug.LogWarning("Turret Hit");
        }
    }
}
