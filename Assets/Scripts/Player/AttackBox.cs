using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBox : MonoBehaviour
{
    public float startup;
    public float damage;
    public float angle;
    public float knockback;
    public BoxCollider box;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        //Debug.Log("box triggered");
        transform.parent.GetComponent<Attack>().TriggerDetected(other);
    }
}
