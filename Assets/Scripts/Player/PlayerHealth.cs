using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    private float health;
    private bool alive;
    public float invulCD = 0.2f;
    public float invulTimer = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        health = 100.0f;
        alive = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(health <= 0){
            alive = false;
            //Destroy(gameObject);
        }
    }

    void ApplyDamage(float[] values)
    {
        // 0 is damage[0], 1 is xMove[0], 2 is yMove[0], 3 is knockback[0]
        if (Time.time > invulTimer)
        {
            invulTimer = Time.time + invulCD;
            Debug.Log("Player Damage Applied");
            health -= values[0];

            //if (playerLoc.position.x - transform.position.x < 0)
            //{
            //    moveDirection.x += values[1];
            //    moveDirection.y = values[2];
            //}
            //else
            //{
            //    moveDirection.x -= values[1];
            //    moveDirection.y = values[2];
            //}
        }

    }
}
