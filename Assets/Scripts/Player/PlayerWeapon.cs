using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    public int level;
    public float weight;
    public Attack[] attacks;
    public Sprite sprite;

    public float boxCD = 0.2f;
    public float boxTimer = 0.0f;

    public Attack currAttack;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    void startAttack(int attackNum) {
        Debug.Log("playerWeapon");
        attacks[attackNum].enable = true;
    }
}
