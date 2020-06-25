using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public int level;
    public float weight;
    public Attack[] attacks;
    public Sprite sprite;
    //public Transform userLocation;
    //public LoadOutManager loadOut;
    public Attack currAttack;
    // Start is called before the first frame update
    void Start()
    {
        //loadOut = GetComponentInParent<LoadOutManager>();
        //userLocation = loadOut.userLocation;
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void InitAttack(int attackNum) {
        //dsDebug.Log("Weapon Active");
        //attacks[attackNum].enable = true;
        attacks[attackNum].StartAttack();
    }
}
