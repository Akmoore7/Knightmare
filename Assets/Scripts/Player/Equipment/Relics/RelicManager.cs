using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RelicManager : MonoBehaviour
{
    public PlayerRelic[] relics;
    public float mana;
    public float manaRegen;
    public ResourceUI manaBar;
    
    void Start()
    {
        manaBar = GameObject.FindGameObjectWithTag("ManaUI").GetComponent<ResourceUI>();
        mana = 100f;
        manaBar.SetMax(mana);
        manaRegen = 0.05f;
    }

    void Update()
    {
        ManaUpdate();
    }

    //Activates specified relic and drains mana, or does nothing because not enough mana.
    public void RelicActivate(int relicNum)
    {
        if (relics[relicNum].manaCost <= mana)
        {
            mana -= relics[relicNum].manaCost;
            relics[relicNum].ActivateRelic();
        }
        else {
            Debug.Log("not enough mana!");
        }
    }

    //Regenerate mana up to a set limit and update the mana UI.
    public void ManaUpdate() {
        if (mana <= 100f)
        {
            mana += manaRegen;
        }
        manaBar.SetValue(mana);
    }
}
