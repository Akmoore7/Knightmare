using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]

public class NPCController : MonoBehaviour
{
    public NPCDamageController damageController;
    public CharacterController characterController;
    public Transform playerLoc;

    public bool alreadyDead;
    public bool alreadyKnocked;
    public bool inHitStun;

    public float hitStunTime = 0f;
    public float xKnockback = 0f;
    public float yKnockback = 0f;
    public float weight;
    public float gravity = 2.0f;

    public Vector3 moveDirection = Vector3.zero;

    void Start()
    {
        alreadyKnocked = true;
        alreadyDead = false;
        inHitStun = false;
        weight = 1.0f;

        playerLoc = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        AttackController();
        MovementController();
    }

    //Controls movement of NPC, overwrite for more interesting movement!
    public virtual void MovementController()
    {
        if (!damageController.alive)
        {
            DeathAct();
        }

        moveDirection.y -= gravity * Time.deltaTime;

        StunController();

        characterController.Move(moveDirection * Time.deltaTime);
    }

    //Controls attack pattern of NPC, overwrite for non-passive behavior!
    public virtual void AttackController()
    {
        if (damageController.alive)
        {
            Debug.Log("uwugugu");
            //weapon.gameObject.SendMessage("InitAttack", curr_attack);

            //characterController.Move(moveDirection * Time.deltaTime);
        }
    }

    //Recieves attack data from player attack and applies hitstun and knockback.
    public virtual void StunController()
    {
        if (inHitStun && (Time.time <= hitStunTime))
        {
            if (!alreadyKnocked) {
                moveDirection.x = xKnockback;
                moveDirection.y = yKnockback;
                alreadyKnocked = true;
            }

            FrictionController();
        }
        else if (Time.time > hitStunTime) {
            //Can be replaced with IEnumerator
            inHitStun = false;
        }
    }

    //Used by player to send attack data to this class, and prepares this object for damage.
    public void HitTrigger(float xMove, float yMove, float hitStun) {
        inHitStun = true;
        alreadyKnocked = false;

        xKnockback = xMove;
        yKnockback = yMove;
        Debug.Log("Hit trigger");

        hitStunTime = Time.time + hitStun;

    }

    //Adds a bit of friction when this object is not in control.
    public void FrictionController()
    {
        if (moveDirection.x > 0)
        {
            moveDirection.x -= 3.0f * Time.deltaTime * weight;
        }
        if (moveDirection.x < 0)
        {
            moveDirection.x += 3.0f * Time.deltaTime * weight;
        }

        if (moveDirection.z > 0)
        {
            moveDirection.z -= 0.5f * Time.deltaTime * weight;
        }
        if (moveDirection.z < 0)
        {
            moveDirection.z += 0.5f * Time.deltaTime * weight;
        }
    }

    //Death Trigger that sends this object into the z-axis.
    public virtual void DeathAct()
    {
        if (!alreadyDead)
        {
            alreadyDead = true;
            float rand = Random.Range(-1.0f, 1.0f);
            //transform.localRotation = Quaternion.Euler(30, 0, 90);
            if (rand >= 0.0f)
            {
                moveDirection.z = -2;
            }
            else
            {
                moveDirection.z = -2;
            }
            characterController.Move(moveDirection * Time.deltaTime);
        }
        else
        {
            FrictionController();
        }
    }


}
