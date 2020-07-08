using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]

public class NPCController : MonoBehaviour
{
    public NPCDamageController damageController;
    public CharacterController characterController;
    public Transform playerLoc;

    private bool alreadyDead;
    private bool alreadyKnocked;
    public bool inHitStun;

    private float hitStunTime = 0f;
    private float xKnockback = 0f;
    private float yKnockback = 0f;
    private float weight;
    public float gravity = 2.0f;

    public Vector3 moveDirection = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        alreadyKnocked = true;
        alreadyDead = false;
        inHitStun = false;
        weight = 1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        attackController();
        movementController();
    }

    public virtual void movementController()
    {
        if (!damageController.alive)
        {
            deathAct();
        }

        moveDirection.y -= gravity * Time.deltaTime;

        stunController();

        characterController.Move(moveDirection * Time.deltaTime);
    }


    public virtual void attackController()
    {
        if (damageController.alive)
        {
            Debug.Log("uwugugu");
            //weapon.gameObject.SendMessage("InitAttack", curr_attack);

            //characterController.Move(moveDirection * Time.deltaTime);
        }
    }

    public void stunController()
    {
        if (inHitStun && (Time.time <= hitStunTime))
        {
            if (!alreadyKnocked) {
                moveDirection.x = xKnockback;
                moveDirection.y = yKnockback;
                alreadyKnocked = true;
            }

            frictionController();
        }
        else if (Time.time > hitStunTime) {
            inHitStun = false;
        }
    }

    public void hitTrigger(float xMove, float yMove, float hitStun) {
        inHitStun = true;
        alreadyKnocked = false;

        xKnockback = xMove;
        yKnockback = yMove;
        Debug.Log("Hit trigger");

        hitStunTime = Time.time + hitStun;

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


    public void deathAct()
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
