using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamageController : MonoBehaviour
{
    public float health;
    public bool alive;
    public float invulCD = 0.2f;
    public float invulTimer = 0.0f;
    public PlayerController player;

    // Start is called before the first frame update
    void Start()
    {
        health = 100.0f;
        alive = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0 && alive)
        {
            alive = false;
            //Destroy(gameObject);
        }
    }

    public void ApplyDamage(float[] values, Transform attackerLoc)
    {
        // 0 is damage[0], 1 is xMove[0], 2 is yMove[0], 3 is knockback[0], 4 is hitstun[]

        if (Time.time > invulTimer)
        {
            invulTimer = Time.time + invulCD;
            health -= values[0];

            //Debug.Log("Enemy Damage Applied");

            float xMove = 0f;
            float yMove = 0f;
            if (attackerLoc.position.x - transform.position.x < 0)
            {
                xMove += values[1];
                yMove += values[2];
            }
            else
            {
                xMove -= values[1];
                yMove += values[2];
            }
            player.hitTrigger(xMove, yMove, values[4]);
        }

    }
}
