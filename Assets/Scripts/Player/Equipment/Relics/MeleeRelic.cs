using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeRelic : PlayerRelic
{
    public float cooldown;
    public float overallTime;
    public string element;

    public float[] startTime;
    public float[] activeTime;
    public float[] damage;
    public float[] knockback;
    public float[] xMove;
    public float[] yMove;

    private float boxCD = 0.2f;
    private float boxTimer = 0.0f;

    private float startUpCD = 0.2f;
    private float startUpTimer = 0.0f;
    private bool started = false;

    public BoxCollider[] boxes;
    public MeshRenderer[] meshes;
    private bool boxActive = false;

    // Start is called before the first frame update
    void Start()
    {
        ranged = false;
        active = true;
        overallTime = startTime[0] + activeTime[0] + cooldown;
    }


    // Update is called once per frame
    void Update()
    {
        if (started && Time.time > startUpTimer)
        {
            Debug.Log("box added");
            started = false;
            boxes[0].enabled = true;
            meshes[0].enabled = true;
            boxActive = true;
            boxCD = activeTime[0];
            boxTimer = Time.time + boxCD;
        }

        if (boxActive && Time.time > boxTimer)
        {
            boxActive = false;
            boxes[0].enabled = false;
            meshes[0].enabled = false;
            Debug.Log("box removed");
        }
    }

    public void TriggerDetected(float damages, Collider Col)
    {
        Debug.Log("hit detected");
        if (Col.gameObject.tag == "Enemy")
        {
            float[] values = { damage[0], xMove[0], yMove[0], knockback[0] };
            Col.gameObject.SendMessage("ApplyDamage", values);
        }
        //float damage, float xMove, float yMove, float knockback, string element
    }

    public new void ActivateRelic()
    {
        Debug.Log("attack enabled");
        startUpCD = startTime[0];
        startUpTimer = Time.time + startUpCD;
        started = true;
    }


}
