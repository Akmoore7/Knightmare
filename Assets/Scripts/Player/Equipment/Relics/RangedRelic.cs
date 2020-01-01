using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedRelic : PlayerRelic
{
    public GameObject projectile;
    // Start is called before the first frame update
    void Start()
    {
        ranged = true;
        active = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public new void ActivateRelic()
    {
        GameObject proj = Instantiate(projectile, transform.position, Quaternion.identity) as GameObject;
        proj.GetComponent<Rigidbody>().AddForce(transform.forward * 10);
    }
}
