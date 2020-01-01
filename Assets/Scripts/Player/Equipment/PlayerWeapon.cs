using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    public int level;
    public float weight;
    public Attack[] attacks;
    public Sprite sprite;

    public Attack currAttack;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    void InitAttack(int attackNum) {
        Debug.Log("playerWeapon");
        //attacks[attackNum].enable = true;
        attacks[attackNum].StartAttack();
    }
}
