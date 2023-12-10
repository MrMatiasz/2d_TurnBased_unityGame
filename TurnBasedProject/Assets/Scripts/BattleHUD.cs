using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleHUD : MonoBehaviour
{
    public Text nameText;
    public Text lvlText;
    public Text hpText;

    public void SetHUD(Unit unit)
    {
        nameText.text = unit.unitName;
        lvlText.text = "Lvl: " + unit.unitLvl;
        hpText.text = "HP: " + unit.currentHP.ToString() + "/" + unit.maxHP.ToString();
    }
}
