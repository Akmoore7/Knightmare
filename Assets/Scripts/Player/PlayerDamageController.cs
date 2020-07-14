using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamageController : MonoBehaviour
{
    public float maxhealth = 100f;
    public float health;
    public bool alive;
    public float invulCD = 0.2f;
    public float invulTimer = 0.0f;
    public PlayerController player;

    public ResourceUI healthBar;
    public ResourceUI manaBar;

    void Start()
    {
        healthBar = GameObject.FindGameObjectWithTag("HealthUI").GetComponent<ResourceUI>();
        health = maxhealth;
        healthBar.SetMax(health);
        alive = true;
    }

    void Update()
    {
        if (health <= 0 && alive)
        {
            alive = false;
        }
    }

    //Used by enemy attacks to send damage to this object, where damage is applied, and the
    //  knockback is sent to the PlayerController.
    public void ApplyDamage(float[] values, Transform attackerLoc)
    {
        // 0 is damage[0], 1 is xMove[0], 2 is yMove[0], 3 is knockback[0], 4 is hitstun[]

        if (Time.time > invulTimer)
        {
            invulTimer = Time.time + invulCD;
            health -= values[0];
            healthBar.SetValue(health - values[0]);
            Debug.Log(health);

            //Debug.Log("Enemy Damage Applied");

            //Finding which direction the attack was recieved from and inverting knockback if necessary.
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
            
            //Sending knockback data to PlayerController
            player.hitTrigger(xMove, yMove, values[4]);
        }

    }
}
