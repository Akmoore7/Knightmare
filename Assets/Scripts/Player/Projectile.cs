using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float[] damage;
    public float[] knockback;
    public float[] xMove;
    public float[] yMove;
    public float[] hitStun;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider Col)
    {
        float[] values = { damage[0], xMove[0], yMove[0], knockback[0], hitStun[0]};
        //Debug.Log("proj hit");

        if (Col.gameObject.tag == "Enemy" && this.tag == "player_attack")
        {
            Debug.Log("Enemy Hit");
            NPCDamageController recipient = Col.gameObject.GetComponent<NPCDamageController>();
            recipient.ApplyDamage(values, this.transform);
            Object.Destroy(this.gameObject);
        }
        else if (Col.gameObject.tag == "Player" && this.tag == "enemy_attack")
        {
            Debug.Log("playerHit");
            PlayerDamageController recipient = Col.gameObject.GetComponent<PlayerDamageController>();
            recipient.ApplyDamage(values, this.transform);
            Object.Destroy(this.gameObject);
        }
        else if (Col.gameObject.tag == "World" && (this.tag == "player_attack" || this.tag == "enemy_attack"))
        {
            Debug.Log("World Collision");
            Object.Destroy(this.gameObject);
        }
        else {
            Debug.Log(Col.tag);
        }
    }
}
