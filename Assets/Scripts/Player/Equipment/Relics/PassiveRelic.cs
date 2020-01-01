using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveRelic : PlayerRelic
{
    // Start is called before the first frame update
    void Start()
    {
        ranged = false;
        active = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public new void ActivateRelic()
    {
        Debug.Log("cant activate, passive relic");
    }
}
