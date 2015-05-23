﻿using UnityEngine;
using System.Collections;

public class ProjectileController : MonoBehaviour {

    public enum ProjectileType
    {
        Small,
        Medium,
        Large,
        Super,
        Smallball,
        Largeball
    }

    public ProjectileType projectileType = ProjectileType.Small;

    private bool firing = false;
    public int speed;
    private int direction;
    private Vector3 startingLoc;

    public int damage = 1;
	
	// Update is called once per frame
	void Update () {
        if (firing)
        {
            transform.position += new Vector3(speed * direction, 0, 0) * Time.deltaTime;

            if (Vector3.Distance(transform.position, startingLoc) > 10.0f)
                KillProjectile();

        }
	}

    public void Fire(Vector3 loc, int dir, float xOffset)
    {
        //offsets here are so that it appear to come directly out of the mouth of the turtle
        transform.position = new Vector3(loc.x + (xOffset * dir), loc.y + 2.10f, loc.z);
        startingLoc = loc;
        direction = dir;
        firing = true;

        if (dir == 1)
        {
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
        }
    }

    public void KillProjectile()
    {
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        
        if (collider.gameObject.tag == "Enemy")
        {
            Debug.Log("collided!");
            collider.gameObject.GetComponent<EnemyController>().Hit(damage);
            KillProjectile();
        }
    }
}
