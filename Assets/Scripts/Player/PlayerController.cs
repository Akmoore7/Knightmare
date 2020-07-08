using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Vector3 moveDirection = Vector3.zero;
    CharacterController characterController;
    public PlayerDamageController health;
    public Animator motionAnimator;
    public SpriteRenderer sprite;
    public GameObject[] hitBoxes;
    public Sprite[] spriteArray;
    public LoadOutManager loadOut;
    public Weapon weapon;
    public RelicManager relicManager;

    private BoxCollider box;
    private MeshRenderer mesh;

    public float speed = 4.0f;
    public float jumpSpeed = 10.0f;
    public float gravity = 10.0f;
    public float weight = 1f;

    public bool newlyGrounded = false;
    public bool alreadyKnocked = true;
    public bool facingRight = true;
    public bool alreadyDead = false;
    public bool inLag = false;
    public bool blocking = false;
    public bool dashing = false;
    public bool dodging = false;
    public bool jumpOne = false;
    public bool jumpTwo = false;

    private float xQueuedKnockback = 0f;
    private float yQueuedKnockback = 0f;
    public float nextAttack = 0f;
    public float attackCD = 1f;
    public float lagTimer = 0f;
    public float lagHelper = 0f;



    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        //motionAnimator.enabled = false;
        //loadOut = GetComponent<LoadOutManager>();
        //weapon = loadOut.getWeapon();
        //relicManager = loadOut.getRelic();
    }

    void Update()
    {
        if (!alreadyDead) {
            boxChecker();
            MovementController();
            CombatController();
        }
    }

    void MovementController()
    {
        DashController();

        if (characterController.isGrounded)
        {
            if (!newlyGrounded) {
                motionAnimator.SetBool("Grounded", true);
                newlyGrounded = true;
                lagTimer = Time.time + 0.07f;
                moveDirection.x *= speed * 0f;

            }
            // We are grounded, so recalculate
            // move direction directly from axes
            if (!inLag)
            {
                //moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));
                moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0.0f, 0.0f);
                moveDirection *= speed;
                switch_sprite();
            }
        }
        else
        {
            if (!inLag)
            {
                moveDirection.x = Input.GetAxis("Horizontal");
                //moveDirection.x *= speed * 0.75f;
                moveDirection.x *= speed * 1f;

                if (moveDirection.x == 0f)
                {
                    motionAnimator.SetBool("isRunning", false);
                }
                else {
                    motionAnimator.SetBool("isRunning", true);
                }
                //motionAnimator.enabled = false;
                //sprite.sprite = spriteArray[2];
                //Fastfalling
                //if (Input.GetAxis("Vertical") < 0)
                //{
                //    moveDirection.y = -7f;
                //}
            }

        }

        moveDirection.y -= gravity * Time.deltaTime;
        JumpController();
        // Apply gravity. Gravity is multiplied by deltaTime twice (once here, and once below
        // when the moveDirection is multiplied by deltaTime). This is because gravity should be applied
        // as an acceleration (ms^-2)

        stunController();

        if (!health.alive) {
            deathAct();
        }

        characterController.Move(moveDirection * Time.deltaTime);
    }

    void CombatController() {
        //DefenseController();
        if (!inLag) {
            WeaponController();
            RelicController();
        }
    }

    void WeaponController()
    {
        if (Input.GetButtonDown("Fire1") && Time.time > nextAttack)
        {
            int curr_attack = 0;
            if (characterController.isGrounded)
            {
                if (!dashing)
                {
                    moveDirection.x *= 0.0f;

                    if ((Input.GetKey(KeyCode.D) && facingRight) || (Input.GetKey(KeyCode.A) && !facingRight))
                    {
                        Debug.Log("f-tilt");
                        curr_attack = 2;
                    }
                    else if (Input.GetKey(KeyCode.W))
                    {
                        Debug.Log("up-tilt");
                        curr_attack = 3;
                    }
                    else if (Input.GetKey(KeyCode.S))
                    {
                        Debug.Log("d-tilt");
                        curr_attack = 4;
                    }
                    else
                    {
                        Debug.Log("jab");
                        curr_attack = 1;
                    }
                }
                else
                {
                    Debug.Log("dash-attack");
                    curr_attack = 0;
                    moveDirection.x *= 0.5f;
                }
            }
            else
            {
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
                else if (Input.GetKey(KeyCode.W))
                {
                    Debug.Log("up air");
                    curr_attack = 11;
                }
                else if (Input.GetKey(KeyCode.S))
                {
                    Debug.Log("down air");
                    curr_attack = 12;
                    moveDirection.y -= 3f;
                }
                else
                {
                    Debug.Log("neutral air");
                    curr_attack = 8;
                }
            }
            //weapon.gameObject.SendMessage("InitAttack", curr_attack);
            weapon.InitAttack(curr_attack);
            LagController(weapon.attacks[curr_attack].overallTime, weapon.attacks[curr_attack].overallTime);
        }
    }

    void RelicController()
    {
        if (Input.GetButtonDown("Fire2") && Time.time > nextAttack)
        {
            int relicAttack = 0;
            if ((Input.GetKey(KeyCode.D) && facingRight) || (Input.GetKey(KeyCode.A) && !facingRight))
            {
                Debug.Log("side-relic");
                relicAttack = 1;
            }
            else if (Input.GetKey(KeyCode.W))
            {
                Debug.Log("up-relic");
                relicAttack = 2;
            }
            else if (Input.GetKey(KeyCode.S))
            {
                Debug.Log("d-relic");
                relicAttack = 3;
            }
            else
            {
                //Debug.Log("neutral-relic");
                relicAttack = 0;
            }

            if (inLag && Time.time > lagTimer)
            {
                inLag = false;
                //box.enabled = false;
                //mesh.enabled = false;
            }
            relicManager.RelicActivate(relicAttack);
            LagController(0.4f, 0.4f);
        }
    }

    void JumpController(){
            //makes sure that you only have 2 jumps, one from ground, and 1 in air
            if (characterController.isGrounded)
            {
                //reset jumps when grounded
                jumpOne = true;
                jumpTwo = true;

                if (Input.GetButtonDown("Jump") && !inLag)
                {
                    motionAnimator.SetBool("isGrounded", false);
                    motionAnimator.SetTrigger("PlayerJump");
                    //motionAnimator.SetBool("Grounded", true);
                    newlyGrounded = false;
                    moveDirection.y = jumpSpeed;
                    jumpOne = false;
            }
            }
            else
            {
                if (Input.GetButtonDown("Jump") && jumpTwo && !inLag)
                {
                    moveDirection.y = jumpSpeed;
                    jumpTwo = false;
                }
            }


        }
    void DashController()
        {
            if (Input.GetKey(KeyCode.LeftShift) && characterController.isGrounded)
            {
                dashing = true;
                speed = 7;
            }
            else if(!Input.GetKey(KeyCode.LeftShift))
            {
                dashing = false;
                speed = 5;
            }
        }

    void switch_sprite() {
            //makes sure sprite is facing correct way, and when still no animation.
            if (moveDirection.x < 0)
            {
                transform.localRotation = Quaternion.Euler(0, 180, 0);
                facingRight = false;
                motionAnimator.SetBool("isRunning", true);
                //motionAnimator.enabled = true;
            }
            else if (moveDirection.x > 0)
            {
                transform.localRotation = Quaternion.Euler(0, 0, 0);
                facingRight = true;
                motionAnimator.SetBool("isRunning", true);
                //motionAnimator.enabled = true;
                //motion_animator.deltaPosition.y += 0.5;
            }
            else
            {
                motionAnimator.SetBool("isRunning", false);
            }

            if (moveDirection.y > 0) {
                //motionAnimator.SetBool("isGrounded", false);
            }
        }

    void DefenseController()
        {
        //add 
            if (Input.GetKey(KeyCode.LeftControl) && Time.time > nextAttack)
            {
                if (characterController.isGrounded)
                {
                    if (Input.GetKey(KeyCode.D))
                    {
                        Debug.Log("roll");
                        moveDirection.x = 3.0f;
                        dodging = true;
                    }
                    else if (Input.GetKey(KeyCode.A))
                    {   
                        Debug.Log("roll");
                        moveDirection.x = -3.0f;
                        dodging = true;
                    }
                    else if (Input.GetKey(KeyCode.S))
                    {
                        Debug.Log("spot-dodge");
                        //moveDirection.x = 3.0f;
                        dodging = true;
                    }
                    else
                    {
                        Debug.Log("neutral-relic");
                        blocking = true;
                    }

                }
                else
                {
                    if (Input.GetKey(KeyCode.D))
                    {
                        Debug.Log("f-air dodge");
                        moveDirection.x = 3.0f;
                        dodging = true;
                    }
                    else if (Input.GetKey(KeyCode.A))
                    {
                        Debug.Log("b-air dodge");
                        moveDirection.x = -3.0f;
                        dodging = true;
                    }
                    else if (Input.GetKey(KeyCode.S))
                    {
                        Debug.Log("d-air dodge");
                        moveDirection.y = -3.0f;
                        dodging = true;
                    }
                    else if (Input.GetKey(KeyCode.W))
                    {
                        Debug.Log("Up-air dodge");
                        moveDirection.y = 3.0f;
                        dodging = true;
                    }
                    else
                    {
                        Debug.Log("n-air dodge");
                        //moveDirection.x = -3.0f;
                        blocking = true;
                    }
                }

                LagController(0.5f, 0.5f);
        }

        }

    void stunController()
    {
        if (inLag)
        {
            if (!alreadyKnocked)
            {
                moveDirection.x = xQueuedKnockback;
                moveDirection.y = yQueuedKnockback;
                alreadyKnocked = true;
            }
            else {
                frictionController();
            }
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
            moveDirection.z -= 1f * Time.deltaTime * weight;
        }
        if (moveDirection.z < 0)
        {
            moveDirection.z += 1f * Time.deltaTime * weight;
        }
    }

    public void hitTrigger(float xMove, float yMove, float hitStun)
    {
        inLag = true;
        alreadyKnocked = false;

        xQueuedKnockback = xMove;
        yQueuedKnockback = yMove;

        lagTimer = Time.time + hitStun;

    }

    void LagController(float attackLag, float boxLag) { 

        nextAttack = Time.time + attackLag;
        //boxCD = 0.4f;
        lagTimer = Time.time + boxLag;
        inLag = true;
    }

    public void deathAct()
    {
        if (!alreadyDead)
        {
            Debug.Log("Dead");
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
        }
    }

    void boxChecker() {
        if (inLag && Time.time > lagTimer)
        {
            inLag = false;
        }
    }


}
