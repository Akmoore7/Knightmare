using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class HardDummy : NPCController
{
    private float lastMove;
    public Weapon weapon;
    public Animator motionAnimator;

    public bool deadGone = false;

    //Dummy follows player within certain range,
    public override void MovementController() {

        if (!damageController.alive)
        {
            DeathAct();
        }
        else if (damageController.alive && !inHitStun) {
            if (characterController.isGrounded)
            {
                float playerDistance = playerLoc.position.x - transform.position.x;
                if (playerDistance < -0.1 && playerDistance > -5)
                {
                    moveDirection.x = -1f;
                }
                else if (playerDistance > 0.1 && playerDistance < 5)
                {
                    moveDirection.x = 1f;
                }
                else
                {
                    moveDirection.x = 0;
                }
            }
        }

        moveDirection.y -= gravity * Time.deltaTime;

        SwitchSprite();
        StunController();

        characterController.Move(moveDirection * Time.deltaTime);
    }

    //Dummy is always sending out an attack box.
    public override void AttackController() {
        weapon.InitAttack(0);
    }

    //Animation triggers for Dummy.
    void SwitchSprite()
    {
        if (!deadGone) {
            if (inHitStun)
            {
                motionAnimator.SetBool("inHitStun", true);
            }
            else
            {
                motionAnimator.SetBool("inHitStun", false);

                //makes sure sprite is facing correct way, and when still no animation.
                if (characterController.isGrounded) {
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
            }
            
            if (alreadyDead)
            {
                deadGone = true;
                motionAnimator.SetTrigger("Dead");
                motionAnimator.SetBool("isRunning", false);
            }
        }
    }
}
