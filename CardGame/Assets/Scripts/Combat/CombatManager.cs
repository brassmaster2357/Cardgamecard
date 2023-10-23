using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CombatManager : MonoBehaviour
    
{
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
        cards.BeginCombat();
        PlayerTurn();
    }
    public void PlayerTurn()
    {
        manaMax++;
        mana = manaMax;
        cards.Draw(5);
        Debug.Log(summons.GetValue(1));
    }

    public void EnemyTurn()
    {
        
        if (cards == null)
        {
            cards = cardManager.GetComponent<PlayerCards>();
        }
        Debug.Log(summons.Length);
        cards.DiscardHand();
        for (int i = 0; i < 8; i++)
        {
            Debug.Log(summons.Length);
            Debug.Log(summons.GetValue(i));
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
        Debug.Log("the turn ended");
    }
}
