using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatManager : MonoBehaviour
{
    public GameObject gameVarHandler;
    private GameVariableHandler vars;
    public GameObject cardsHandling;
    private CardsScript cardVar;

    //currentTurn counts how many enemies need to take their turn before the player
    public int currentTurn = 0;

    public GameObject player;
    public GameObject enemy1;
    private EnemyManager enemy1manager;
    public GameObject enemy2;
    private EnemyManager enemy2manager;
    public GameObject enemy3;
    private EnemyManager enemy3manager;

    public GameObject card;

    // Start is called before the first frame update
    void Awake()
    {
        vars = gameVarHandler.GetComponent<GameVariableHandler>();
        cardVar = cardsHandling.GetComponent<CardsScript>();
        
        // Add enemy managers unless there's no enemy
        if (enemy1 != null)
        {
            enemy1manager = enemy1.GetComponent<EnemyManager>();
        }
        if (enemy2 != null)
        {
            enemy2manager = enemy2.GetComponent<EnemyManager>();
        }
        if (enemy3 != null)
        {
            enemy3manager = enemy3.GetComponent<EnemyManager>();
        }

        DrawCards();
    }

    // Update is called once per frame
    void Update()
    {
        // Kill enemies that run out of HP, no matter when
        if (vars.enemy1HP <= 0)
        {
            Destroy(enemy1);
            enemy1 = null;
        }
        if (vars.enemy2HP <= 0)
        {
            Destroy(enemy1);
            enemy1 = null;
        }
        if (vars.enemy3HP <= 0)
        {
            Destroy(enemy1);
            enemy1 = null;
        }

        // Test script: Force turn end with "Y"
        if (Input.GetKeyDown(KeyCode.Y))
        {
            EndTurn();
        }
    }

    //This function will be called when the enemy turns are finished
    public void DrawCards()
    {
        //Make enemies decide their action
        if (enemy1 != null)
        {
            enemy1manager.DecideAction();
        }
        if (enemy2 != null)
        {
            enemy2manager.DecideAction();
        }
        if (enemy3 != null)
        {
            enemy3manager.DecideAction();
        }

        //Draw cards
        for (int i = 0; i < vars.cardDrawOnTurn; i++)
        {
            //If draw pile is empty, shuffle discard, otherwise draw from draw pile
            if (vars.playerDraw.Count == 0)
            {
                vars.playerDraw = vars.playerDiscard;
                vars.playerDiscard.Clear();
            }
            else
            {
                vars.playerHand.Add(vars.playerDraw[0]);
                GameObject cardDrawn = Instantiate(card, Vector3.down * 10, Quaternion.identity);
                cardDrawn.GetComponent<SpriteRenderer>().sprite = (Sprite)cardVar.cardSprites.GetValue(vars.playerDraw[0]);
                cardDrawn.GetComponent<CardsScript>().cardType = (string)cardVar.cardName.GetValue(vars.playerDraw[0]);
                vars.playerDraw.RemoveAt(0);
            }
        }
    }
    
    public void EndTurn()
    {
        vars.playerDiscard.AddRange(vars.playerHand);
        vars.playerHand.Clear();
        // If there is an enemy, start their turn and wait until they're done, then draw new cards and start player's turn again
        /*if (enemy1 != null)
        {
            yield return StartCoroutine(nameof(Enemy1turn));
        }
        if (enemy2 != null)
        {
            yield return StartCoroutine(nameof(Enemy2turn));
        }
        if (enemy3 != null)
        {
            yield return StartCoroutine(nameof(Enemy3turn));
        }*/
        if (enemy1 != null)
        {
            Enemy1turn();
        }
        if (enemy2 != null)
        {
            Enemy2turn();
        }
        if (enemy3 != null)
        {
            Enemy3turn();
        }
        DrawCards();
    }
    private string Enemy1turn()
    {
        return enemy1.GetComponent<EnemyManager>().Action();
    }
    private string Enemy2turn()
    {
        return enemy2.GetComponent<EnemyManager>().Action();
    }
    private string Enemy3turn()
    {
        return enemy3.GetComponent<EnemyManager>().Action();
    }

    public void PlayCard(GameObject cardPlayed, string cardType, string target)
    {
        if (cardType == "Attack")
        {
            if (target == "enemy1")
            {
                vars.enemy1HP -= 5f;
                Destroy(cardPlayed);
            } else if (target == "enemy2")
            {
                vars.enemy2HP -= 5;
                Destroy(cardPlayed);
            } else if (target == "enemy3")
            {
                vars.enemy3HP -= 5;
                Destroy(cardPlayed);
            }
        } else if (cardType == "Heal")
        {
            vars.playerHP += 5;
            Destroy(cardPlayed);
        }
        
    }
}
