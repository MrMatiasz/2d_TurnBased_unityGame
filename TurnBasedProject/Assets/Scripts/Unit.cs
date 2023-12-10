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

    double critValue = 0.10;
    
    bool isCrit;

    public bool TakeDMG(int damage)
    {
        double crit = damage * critValue;

        int random = Random.Range(1, 10);
        if (random == 1)
        {
            currentHP -= damage + (int)crit;
        }

        else
        {
            currentHP -= damage;
        }

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
        int randomHeal = Random.Range(3, 9);
        currentHP = currentHP + randomHeal;

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