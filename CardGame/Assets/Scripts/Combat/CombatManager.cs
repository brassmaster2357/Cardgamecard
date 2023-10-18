using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatManager : MonoBehaviour
    
{
    public GameObject cardManager;
    private PlayerCards cards;

    private void Awake()
    {
        cards = cardManager.GetComponent<PlayerCards>();
        cards.Draw(5);
    }
    public void PlayerTurn()
    {
        cards.Draw(5);
    }

    public void EnemyTurn()
    {
        cards.DiscardHand();
    }
}
