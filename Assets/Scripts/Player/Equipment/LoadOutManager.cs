using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadOutManager : MonoBehaviour
{
    public Weapon weapon;
    public RelicManager relics;
    //public Transform userLocation;

    // Start is called before the first frame update
    void Start()
    {
        //userLocation = GetComponentInParent<Transform>();
        //weapon = GetComponent<Weapon>();
        //relics = GetComponent<RelicManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Weapon getWeapon() {
        return (weapon);
    }

    public RelicManager getRelic()
    {
        return (relics);
    }
}
