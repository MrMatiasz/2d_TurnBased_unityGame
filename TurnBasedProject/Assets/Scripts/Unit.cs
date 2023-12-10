using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public string unitName;
    public int unitLvl;

    public int damage;

    public int maxHP;
    public int currentHP;

    public bool TakeDMG(int damage)
    {
        currentHP -= damage;

        if(currentHP <= 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool TakeHeal()
    {
        currentHP = currentHP + 5;

        if(currentHP < maxHP)
        {
            return false;
        }

        else
        {
            return false;
        }
    }
}