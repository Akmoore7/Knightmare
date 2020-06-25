using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyZombieAI : MonoBehaviour
{
    public NPCDamageController enemyLife;
    public CharacterController characterController;
    private Vector3 moveDirection = Vector3.zero;
    public Transform playerLoc;
    private bool alreadyDead;
    private bool hitStun;
    private float weight;

    public float gravity = 15.0f;
    // Start is called before the first frame update
    void Start()
    {
        alreadyDead = false;
        hitStun = false;
        weight = 1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        movementController();
    }

    void movementController()
    {
        moveDirection.y -= gravity * Time.deltaTime;
        if (!enemyLife.alive && !hitStun)
        {
            float playerDistance = playerLoc.position.x - transform.position.x;

            if (playerDistance < -1.5 && playerDistance > -5)
            {
                moveDirection.x = -0.75f;
            }
            else if (playerDistance > 1.5 && playerDistance < 5)
            {
                moveDirection.x = 0.75f;
            }
        }
        else if(!enemyLife.alive)
        {
                deathAct();
        }

        stunController();
        characterController.Move(moveDirection * Time.deltaTime);

        // ADD HITSTUN CONTROLLER (zombie doesnt move itself during hitstun and has friction)
    }


    void attackController() {
        if (enemyLife.alive)
        {
            //weapon.gameObject.SendMessage("InitAttack", curr_attack);

            //characterController.Move(moveDirection * Time.deltaTime);
        }
    }

    void stunController() {
        if (hitStun) {
            frictionController();
        }
    }

    void frictionController()
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
            moveDirection.z -= 1.0f * Time.deltaTime * weight;
        }
        if (moveDirection.z < 0)
        {
            moveDirection.z += 1.0f * Time.deltaTime * weight;
        }
    }

    void deathAct()
    {
        if (!alreadyDead)
        {
            alreadyDead = true;
            float rand = Random.Range(-1.0f, 1.0f);
            transform.localRotation = Quaternion.Euler(30, 0, 90);
            if (rand >= 0.0f)
            {
                moveDirection.z = 2;
            }
            else
            {
                moveDirection.z = -2;
            }
            characterController.Move(moveDirection * Time.deltaTime);
        }
    }


}
