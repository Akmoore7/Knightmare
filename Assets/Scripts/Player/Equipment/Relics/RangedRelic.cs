 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

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

    public override void ActivateRelic()
    {
        //Debug.Log("relic start");
        GameObject proj = Instantiate(projectile, transform.position, Quaternion.identity) as GameObject;
        proj.GetComponent<MeshRenderer>().enabled = true;
        proj.GetComponent<SphereCollider>().enabled = true;
        proj.GetComponent<Rigidbody>().velocity = transform.TransformDirection(Vector3.right * 10);
        //proj.GetComponent<Attack>().StartAttack();
    }
}
