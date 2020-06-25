using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class HardDummy : NPCController
{
    private float lastMove;
    public Weapon weapon;

    public override void movementController() {

        if (!damageController.alive)
        {
            deathAct();
        }
        else if (damageController.alive && !inHitStun) {
            float playerDistance = playerLoc.position.x - transform.position.x;

            if (playerDistance < -0.5 && playerDistance > -5)
            {
                moveDirection.x = -0.75f;
                lastMove = -0.75f;
            }
            else if (playerDistance > 0.5 && playerDistance < 5)
            {
                moveDirection.x = 0.75f;
                lastMove = 0.75f;
            }
            else {
                //moveDirection.x = lastMove;
            }
        }

        moveDirection.y -= gravity * Time.deltaTime;

        stunController();

        characterController.Move(moveDirection * Time.deltaTime);
    }

    public override void attackController() {
        int curr_attack = 0;
        float playerDistance = playerLoc.position.x - transform.position.x;
        weapon.InitAttack(0);
    }
}
