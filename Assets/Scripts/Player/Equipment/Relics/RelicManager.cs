using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RelicManager : MonoBehaviour
{
    public PlayerRelic[] relics;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void RelicActivate(int relicNum)
    {
        Debug.Log("relic");
        relics[relicNum].ActivateRelic();
    }
}
