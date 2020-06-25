using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCDamageController : MonoBehaviour
{
    CharacterController characterController;
    public BoxCollider box;
    public float health = 100.0f;

    public float invulCD = 0.2f;
    public float invulTimer = 0.0f;

    public Vector3 moveDirection = Vector3.zero;

    public NPCController currUnit;

    //public Transform attackerLoc;

    public bool alive;


    // Start is called before the first frame update
    void Start()
    {
        box = GetComponent<BoxCollider>();
        characterController = GetComponent<CharacterController>();
        alive = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0 && alive) {
            //Destroy(this.gameObject);
            alive = false;
        }
    }

    public void ApplyDamage(float[] values, Transform attackerLoc) {
        // 0 is damage[0], 1 is xMove[0], 2 is yMove[0], 3 is knockback[0], 4 is hitstun[]
        //attackerLoc = values[3];
        if (Time.time > invulTimer){
            invulTimer = Time.time + invulCD;
            health -= values[0];

            //Debug.Log("Enemy Damage Applied");

            float xMove = 0f;
            float yMove = 0f;
            if (attackerLoc.position.x - transform.position.x < 0){
                xMove += values[1];
                yMove += values[2];
            }
            else {
                xMove -= values[1];
                yMove += values[2];
            }

            currUnit.hitTrigger(xMove, yMove, values[4]);
            //Debug.Log("Knockback Applied");
        }
        //characterController.Move(moveDirection * Time.deltaTime);

    }

    void OnTriggerEnter(Collider col) {
        if (col.gameObject.tag == "player_attack" && Time.time > invulTimer)
        {
            //invulTimer = Time.time + invulCD;
            //Debug.Log("HIT trig");
            //health -= 15;
            //moveDirection.x = 3.0f;
            //moveDirection.y = 5.0f;
            //moveDirection.y += 5.0f;
        }

        //Debug.Log("notag trig");
    }

    void movementController() {
        
        characterController.Move(moveDirection * Time.deltaTime);
    }
}
