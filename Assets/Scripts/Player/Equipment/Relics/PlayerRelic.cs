using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class PlayerRelic : MonoBehaviour
{
    public bool ranged = false;
    public bool active = true;
    public bool enable = false;
    public int relicSpot = 0;
    public float manaCost = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public virtual void ActivateRelic() {
        Debug.Log("activate base relic");
    }
}
