using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum BattleState { START, PLAYERTURN, ENEMYTURN, WON, LOST, ESCAPE }

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
        int enemyChoose = Random.Range(1, 10);

        dialogText.text = "Enemy turn!";

        if (battleState != BattleState.ENEMYTURN)
        {
            return;
        }

        else
        {
            if(enemyUnit.currentHP == enemyUnit.maxHP)
            {
                StartCoroutine(EnemyAttack());
            }
            
            else if(enemyUnit.currentHP != enemyUnit.maxHP && enemyChoose == 1)
            {
                StartCoroutine(EnemyHeal());
            }

            else if(enemyUnit.currentHP < 15 && enemyChoose < 4)
            {
                StartCoroutine(EnemyHeal());
            }

            else
            {
                StartCoroutine(EnemyAttack());
            }
        }
    }


    IEnumerator PlayerAttack()
    {
        bool isDead = enemyUnit.TakeDMG(playerUnit.damage);

        enemyHUD.SetHUD(enemyUnit);

        if (enemyUnit.isMissed)
        {
            dialogText.text = "Hit was missed!";
        }
        else
        {
            dialogText.text = "attack is successful!";
        }

        yield return new WaitForSeconds(2f);

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
        if (playerUnit.isMissed)
        {
            dialogText.text = "Hit was missed!";
        }
        else
        {
            dialogText.text = "Enemy attack is successful! ";
        }

        yield return new WaitForSeconds(2f);

        if (isDead)
        {
            battleState = BattleState.LOST;
            EndBattle();
        }
        else
        {
            battleState = BattleState.PLAYERTURN;
            PlayerTurn();
        }
    }

    IEnumerator EnemyHeal()
    {
        bool canTake = enemyUnit.TakeHeal();

        enemyHUD.SetHUD(enemyUnit);
        dialogText.text = "Enemy use healing!";

        yield return new WaitForSeconds(2f);

        battleState = BattleState.PLAYERTURN;
        PlayerTurn();
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

        else if(battleState == BattleState.ESCAPE)
        {
            dialogText.text = "Spierdalasz jak pizdeczka";
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

    public void OnEscapeButton()
    {
        battleState = BattleState.ESCAPE;
        EndBattle();
    }
}
