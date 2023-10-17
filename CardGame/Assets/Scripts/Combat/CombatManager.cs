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
    }
    public void PlayerTurn()
    {
        cards.Draw(3);
    }

    public void EnemyTurn()
    {
        cards.DiscardHand();
    }
}
