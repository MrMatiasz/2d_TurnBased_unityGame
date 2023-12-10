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

    public bool isMissed = false;

    public bool TakeDMG(int damage)
    {
        isMissed = false;
        double crit = damage * critValue;

        int random = Random.Range(1, 101);
        int miss = Random.Range(1, 101);

        if (random <= 10)
        {
            currentHP -= damage + (int)crit;
        }

        else if(miss < 50)
        {
            isMissed = true;
            currentHP -= 0;
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