using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CombatManager : MonoBehaviour
    
{
    private EnemyManager enemy;
    public GameObject enemyManager;

    public GameObject cardManager;
    private PlayerCards cards;
    public GameObject[] summons;

    public TextMeshProUGUI drawDisplay;
    public TextMeshProUGUI discardDisplay;
    public TextMeshProUGUI manaDisplay;

    public int mana;
    public int manaMax = 0;

    private void Update()
    {
        drawDisplay.text = cards.drawPile.Count.ToString();
        discardDisplay.text = cards.discardPile.Count.ToString();
        manaDisplay.text = mana.ToString() + "/" + manaMax;
    }

    private void Awake()
    {
        cards = cardManager.GetComponent<PlayerCards>();
        enemy = enemyManager.GetComponent<EnemyManager>();
        cards.BeginCombat();
        PlayerTurn();
    }
    public void PlayerTurn()
    {
        manaMax++;
        mana = manaMax;
        cards.Draw(5);
        cards.OrganizeHand();
    }

    public void EnemyTurn()
    {
        mana = 0;
        if (cards == null)
        {
            cards = cardManager.GetComponent<PlayerCards>();
        }
        cards.DiscardHand();
        for (int i = 0; i < 8; i++)
        {
            SummonScript summon = summons[i].GetComponent<SummonScript>();
            if (summon.alive)
            {
                if (summon.canAttack)
                {
                    summon.Attack();
                }
                else
                {
                    summon.canAttack = true;
                }
            }
        }
        enemy.EnemyTurn();
        PlayerTurn();
    }
}
