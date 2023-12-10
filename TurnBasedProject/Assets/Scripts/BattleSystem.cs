using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum BattleState { START, PLAYERTURN, ENEMYTURN, WON, LOST }

public class BattleSystem : MonoBehaviour
{
    public BattleState battleState;

    public GameObject playerPref;
    public GameObject enemyPref;

    public Transform playerBattleStation;
    public Transform enemyBattleStation;

    public BattleHUD playerHUD;
    public BattleHUD enemyHUD;

    Unit playerUnit;
    Unit enemyUnit;

    public Text dialogText;

    private void Start()
    {
        battleState = BattleState.START;
        StartCoroutine(SetupBattle());
    }

    IEnumerator SetupBattle()
    {
        GameObject playerGO = Instantiate(playerPref, playerBattleStation);
        playerUnit = playerGO.GetComponent<Unit>();

        GameObject enemyGO = Instantiate(enemyPref, enemyBattleStation);
        enemyUnit = enemyGO.GetComponent<Unit>();

        dialogText.text = enemyUnit.unitName + " gets spawned";

        playerHUD.SetHUD(playerUnit);
        enemyHUD.SetHUD(enemyUnit);

        yield return new WaitForSeconds(2f);

        battleState = BattleState.PLAYERTURN;
        PlayerTurn();
    }

    void PlayerTurn()
    {
        dialogText.text = "Choose an action: ";
    }
    void EnemyTurn()
    {
        dialogText.text = "Enemy turn!";

        if (battleState != BattleState.ENEMYTURN)
        {
            return;
        }

        else
        {
            StartCoroutine(EnemyAttack());
        }
    }


    IEnumerator PlayerAttack()
    {
        bool isDead = enemyUnit.TakeDMG(playerUnit.damage);

        enemyHUD.SetHUD(enemyUnit);
        dialogText.text = "attack is successful!";

        yield return new WaitForSeconds(1f);

        if (isDead)
        {
            battleState = BattleState.WON;
            EndBattle();
        }
        else
        {
            battleState = BattleState.ENEMYTURN;
            EnemyTurn();
        }
    }

    IEnumerator PlayerHeal()
    {
        bool canTake = playerUnit.TakeHeal();

        playerHUD.SetHUD(playerUnit);
        dialogText.text = "Healing is successful!";

        yield return new WaitForSeconds(2f);

        battleState = BattleState.ENEMYTURN;
        EnemyTurn();
    }

    IEnumerator EnemyAttack()
    {
        bool isDead = playerUnit.TakeDMG(enemyUnit.damage);

        playerHUD.SetHUD(playerUnit);
        dialogText.text = "Enemy attack is successful!";

        yield return new WaitForSeconds(1f);

        if (isDead)
        {
            battleState = BattleState.LOST;
            EndBattle();
        }
        else
        {
            battleState = BattleState.PLAYERTURN;
            EnemyTurn();
        }
    }

    void EndBattle()
    {
        if(battleState == BattleState.WON)
        {
            dialogText.text = "Congratulation you won this battle!";
        }

        else if(battleState == BattleState.LOST)
        {
            dialogText.text = "Sadlly you lose this fight :(";
        }
    }

    public void OnAttackButton()
    {
        if(battleState != BattleState.PLAYERTURN)
        {
            return;
        }

        else
        {
            StartCoroutine(PlayerAttack());
        }
    }

    public void OnHealButton()
    {
        if (battleState != BattleState.PLAYERTURN)
        {
            return;
        }

        else
        {
            StartCoroutine(PlayerHeal());
        }
    }
}
