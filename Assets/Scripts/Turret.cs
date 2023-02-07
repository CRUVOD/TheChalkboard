using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour {

    public Transform gunTip;
    public Rigidbody2D bullet;
    public float fireRate;
    public float bulletSpeedX;
    public float bulletSpeedY;
    public float bulletAliveTime;
    
    private float nextFire = 0f;
    

	void Start ()
    {
		
	}
	
	
	void Update ()
    {
        FireBlaster();
	}

    void FireBlaster()
    {
        if(Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            
            {
                Rigidbody2D bulletInstance;
                bulletInstance = Instantiate(bullet, gunTip.position, gunTip.rotation) as Rigidbody2D;
                bulletInstance.velocity = new Vector2(bulletSpeedX, bulletSpeedY);
                bulletInstance.GetComponent<ProjectileController>().aliveTime = bulletAliveTime;

                bulletInstance.MovePosition(bulletInstance.position + bulletInstance.velocity * Time.deltaTime);

                
            }
            
        }
    }
}
