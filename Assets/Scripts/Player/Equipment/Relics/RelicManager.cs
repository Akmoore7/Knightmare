using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RelicManager : MonoBehaviour
{
    public PlayerRelic[] relics;
    public float mana;
    public float manaRegen;
    
    // Start is called before the first frame update
    void Start()
    {
        mana = 100f;
        manaRegen = 0.05f;
    }

    // Update is called once per frame
    void Update()
    {
        if (mana <= 100f) {
            mana += manaRegen;
        }
    }

    public void RelicActivate(int relicNum)
    {
        //Debug.Log("relic manager");
        if (relics[relicNum].manaCost <= mana)
        {
            mana -= relics[relicNum].manaCost;
            relics[relicNum].ActivateRelic();
        }
        else {
            Debug.Log("not enough mana!");
        }
    }
}
