using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class RangedBat : NPCController
{
    private float lastMove;
    public Weapon weapon;
    public Animator motionAnimator;
    public GameObject projectile;

    public float leftBoundary;
    public float rightBoundary;
    public float yTarget;

    public bool deadGone = false;
    public bool leftMove = true;
    public bool attacking = false;


    public override void Start()
    {
        alreadyKnocked = true;
        alreadyDead = false;
        inHitStun = false;
        weight = 1.0f;

        playerLoc = GameObject.FindGameObjectWithTag("Player").transform;

        leftBoundary = transform.position.x - 3.0f;
        rightBoundary = transform.position.x + 3.0f;
        yTarget = transform.position.y;
        moveDirection.x = -1f;
        leftMove = true;
        attacking = false;

    }

    //Bat floats between two boundaries and corrects to original altitude
    public override void MovementController()
    {

        if (!damageController.alive)
        {
            moveDirection.y -= gravity * Time.deltaTime;
            DeathAct();
        }
        else if (damageController.alive && !inHitStun)
        {
            //set so hitstun doesnt cancel movement
            if (leftMove)
            {
                moveDirection.x = -1f;
            }
            else
            {
                moveDirection.x = 1f;
            }

            if (transform.position.x < leftBoundary)
            {
                leftMove = false;
            }
            else if (transform.position.x > rightBoundary)
            {
                leftMove = true;
            }

            if(transform.position.y < yTarget)
            {
                moveDirection.y = 1f;
            }
            else if (transform.position.y > yTarget)
            {
                moveDirection.y = -1f;
            }

        }

        if (inHitStun){
            moveDirection.y -= gravity * Time.deltaTime;
        }


        

        SwitchSprite();
        StunController();

        characterController.Move(moveDirection * Time.deltaTime);
    }

    //Dummy is always sending out an attack box.
    public override void AttackController()
    {
        //weapon.InitAttack(0);
        float playerDistance = playerLoc.position.x - transform.position.x;
        if(playerDistance < 5 && playerDistance > -5 && !attacking && !deadGone)
        {
            StartCoroutine("ShootProjectile");
        }

    }

    //Animation triggers for Dummy.
    void SwitchSprite()
    {
        if (!deadGone)
        {
            if (inHitStun)
            {
                motionAnimator.SetBool("inHitStun", true);
            }
            else
            {
                motionAnimator.SetBool("inHitStun", false);

                //makes sure sprite is facing correct way, and when still no animation.
                if (moveDirection.x < 0)
                {
                    transform.localRotation = Quaternion.Euler(0, 180, 0);
                    motionAnimator.SetBool("isRunning", true);
                }
                else if (moveDirection.x > 0)
                {
                    transform.localRotation = Quaternion.Euler(0, 0, 0);
                    motionAnimator.SetBool("isRunning", true);
                }
                else
                {
                    motionAnimator.SetBool("isRunning", false);
                }
            }

            if (alreadyDead)
            {
                deadGone = true;
                motionAnimator.SetTrigger("Dead");
                motionAnimator.SetBool("isRunning", false);
                motionAnimator.SetBool("inHitStun", false);
            }
        }
    }

    IEnumerator ShootProjectile()
    {
        GameObject proj = Instantiate(projectile, transform.position, Quaternion.identity) as GameObject;
        proj.GetComponent<MeshRenderer>().enabled = true;
        proj.GetComponent<SphereCollider>().enabled = true;
        proj.GetComponent<Rigidbody>().velocity = transform.TransformDirection(Vector3.right * 5);
        motionAnimator.SetTrigger("Attack");
        attacking = true;
        yield return new WaitForSeconds(2f);
        attacking = false;
    }

}
