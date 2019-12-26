using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    CharacterController characterController;
    public SpriteRenderer sprite;
    public SpriteRenderer shadow;
    public Animator motionAnimator;

    public float speed = 4.0f;
    public float jumpSpeed = 10.0f;
    public float gravity = 10.0f;
    public bool jumpOne = false;
    public bool jumpTwo = false;
    public bool facingRight = true;
    private bool boxBool = false;

    public float attackCD = 1.0f;
    public float nextAttack = 0.0f;
    public float boxCD = 0.2f;
    public float boxTimer = 0.0f;

    public PlayerHealth health;

    public PlayerWeapon weapon;


    private Vector3 moveDirection = Vector3.zero;
    public Sprite[] spriteArray;
    public GameObject[] hitBoxes;

    private BoxCollider box;
    private MeshRenderer mesh;

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        motionAnimator.enabled = false;
        //box = hitBoxes[3].GetComponent<BoxCollider>();
        //mesh = hitBoxes[3].GetComponent<MeshRenderer>();
        //sprite = GetComponent<SpriteRenderer>();
    }

    void Update() {
        movementController();
        combatController();
    }

    void movementController() {
        if (characterController.isGrounded){
            // We are grounded, so recalculate
            // move direction directly from axes
            if (Time.time > boxTimer) {
                //moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));
                moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0.0f, 0.0f);
                moveDirection *= speed;
                switch_sprite();
            }
        }
        else{
            moveDirection.x = Input.GetAxis("Horizontal");
            moveDirection.x *= speed * 0.75f;
            motionAnimator.enabled = false;
            sprite.sprite = spriteArray[2];
            if (Input.GetAxis("Vertical") < 0)
            {
                moveDirection.y = -7f;
            }

        }

        moveDirection.y -= gravity * Time.deltaTime;
        jumpController();
        // Apply gravity. Gravity is multiplied by deltaTime twice (once here, and once below
        // when the moveDirection is multiplied by deltaTime). This is because gravity should be applied
        // as an acceleration (ms^-2)

        characterController.Move(moveDirection * Time.deltaTime);
    }

    void combatController(){
        if (Input.GetButtonDown("Fire1") && Time.time > nextAttack){
            int curr_attack = 0;
            if (characterController.isGrounded){
                moveDirection.x *= 0.1f;
                if ((Input.GetKey(KeyCode.D) && facingRight) || (Input.GetKey(KeyCode.A) && !facingRight)){
                    Debug.Log("f-tilt");
                    curr_attack = 2;
                }
                else if (Input.GetKey(KeyCode.W)){
                    Debug.Log("up-tilt");
                    curr_attack = 3;
                }
                else if (Input.GetKey(KeyCode.S)){
                    Debug.Log("d-tilt");
                    curr_attack = 4;
                }
                else {
                    Debug.Log("jab");
                    curr_attack = 1;
                }
            }
            else{
                //moveDirection.x *= 0.1f;
                if ((Input.GetKey(KeyCode.D) && facingRight) || (Input.GetKey(KeyCode.A) && !facingRight))
                {
                    Debug.Log("forward air");
                    curr_attack = 9;
                }
                else if ((Input.GetKey(KeyCode.A) && facingRight) || (Input.GetKey(KeyCode.D) && !facingRight))
                {
                    Debug.Log("back air");
                    curr_attack = 10;
                }
                else if (Input.GetKey(KeyCode.W)){
                    Debug.Log("up air");
                    curr_attack = 11;
                }
                else if (Input.GetKey(KeyCode.S)) {
                    Debug.Log("down air");
                    curr_attack = 12;
                }
                else{
                    Debug.Log("neutral air");
                    curr_attack = 8;
                }
            }
            nextAttack = Time.time + weapon.attacks[curr_attack].overallTime;
            weapon.gameObject.SendMessage("startAttack", curr_attack);
            boxCD = weapon.attacks[curr_attack].overallTime;
            boxTimer = Time.time + boxCD;
            boxBool = true;
        }

        if (boxBool && Time.time > boxTimer) {
            boxBool = false;
            //box.enabled = false;
            //mesh.enabled = false;
        }
    }

    void jumpController() {
        //makes sure that you only have 2 jumps, one from ground, and 1 in air
        if (characterController.isGrounded) {
            //reset jumps when grounded
            jumpOne = true;
            jumpTwo = true;

            if (Input.GetButtonDown("Jump")){
                moveDirection.y = jumpSpeed;
                jumpOne = false;
            }
        }
        else {
            if (Input.GetButtonDown("Jump") && jumpTwo){
                moveDirection.y = jumpSpeed;
                jumpTwo = false;
            }
        }


    }

    void switch_sprite() {
        //makes sure sprite is facing correct way, and when still no animation.
        if (moveDirection.x < 0)
        {
            //sprite.flipX = true;
            //shadow.flipX = true;
            transform.localRotation = Quaternion.Euler(0, 180, 0);
            facingRight = false;
            motionAnimator.enabled = true;
        }
        else if (moveDirection.x > 0)
        {
            //sprite.flipX = false;
            //shadow.flipX = false;
            transform.localRotation = Quaternion.Euler(0, 0, 0);
            facingRight = true;
            motionAnimator.enabled = true;
            //motion_animator.deltaPosition.y += 0.5;
        }
        else
        {
            motionAnimator.enabled = false;
            sprite.sprite = spriteArray[0];
            //sprite.transform.z
        }
    }
}
