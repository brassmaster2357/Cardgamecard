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
    public int manaMax = 1;

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
    }
    public void PlayerTurn()
    {
        cards.Draw(5);
    }

    public IEnumerator EnemyTurn()
    {
        cards.DiscardHand();
        for (int i = 0; i < summons.Length; i++)
        {
            SummonScript summon = (SummonScript)summons.GetValue(i);
            if (summon.alive)
            {
                if (summon.canAttack)
                {
                    yield return summon.StartCoroutine("Attack");
                }
                else
                {
                    summon.canAttack = true;
                }
            }
        }
    }
}
