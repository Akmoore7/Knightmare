using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageController : MonoBehaviour
{
    CharacterController characterController;
    public BoxCollider box;
    public float health = 100.0f;

    public float speed = 4.0f;
    public float jumpSpeed = 3.0f;
    public float gravity = 15.0f;

    public float invulCD = 0.2f;
    public float invulTimer = 0.0f;

    public Vector3 moveDirection = Vector3.zero;

    public Transform playerLoc;

    private bool alive;


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

            float rand = Random.Range(-1.0f, 1.0f);
            transform.localRotation = Quaternion.Euler(30, 0, 90);
            if (rand >= 0.0f)
            {
                moveDirection.z = 2;
            }
            else {
                moveDirection.z = -2;
            }
        }
        movementController();
    }

    void ApplyDamage(float[] values) {
        // 0 is damage[0], 1 is xMove[0], 2 is yMove[0], 3 is knockback[0]
        if (Time.time > invulTimer){
            invulTimer = Time.time + invulCD;
            Debug.Log("Enemy Damage Applied");
            health -= values[0];

            if (playerLoc.position.x - transform.position.x < 0){
                moveDirection.x += values[1];
                moveDirection.y = values[2];
            }
            else {
                moveDirection.x -= values[1];
                moveDirection.y = values[2];
            }
        }

    }

    void OnTriggerEnter(Collider col) {
        if (col.gameObject.tag == "player_attack" && Time.time > invulTimer)
        {
            //invulTimer = Time.time + invulCD;
            Debug.Log("HIT trig");
            //health -= 15;
            //moveDirection.x = 3.0f;
            //moveDirection.y = 5.0f;
            //moveDirection.y += 5.0f;
        }

        //Debug.Log("notag trig");
    }

    void movementController() {
        frictionController();
        moveDirection.y -= gravity * Time.deltaTime;
        characterController.Move(moveDirection * Time.deltaTime);
    }

    void frictionController() {
        if (moveDirection.x > 0)
        {
            moveDirection.x -= 3.0f * Time.deltaTime;
        }
        if (moveDirection.x < 0)
        {
            moveDirection.x += 3.0f * Time.deltaTime;
        }

        if (moveDirection.z > 0)
        {
            moveDirection.z -= 1.0f * Time.deltaTime;
        }
        if (moveDirection.z < 0)
        {
            moveDirection.z += 1.0f * Time.deltaTime;
        }
    }
}
