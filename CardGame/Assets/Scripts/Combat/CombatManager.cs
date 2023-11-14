using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class CombatManager : MonoBehaviour
    
{
    private EnemyManager enemy;
    public GameObject enemyManager;
    private PlayerManager player;
    public GameObject playerManager;

    private PlayerCards cards;
    public GameObject[] summons;

    public TextMeshProUGUI drawDisplay;
    public TextMeshProUGUI discardDisplay;
    public TextMeshProUGUI manaDisplay;
    public TextMeshProUGUI victoryScreen;
    public Canvas hud;

    public List<string> victoryTextNormal;
    public List<string> victoryTextDeranged;
    public int mana;
    public int manaMax = 0;

    private void Update()
    {
        drawDisplay.text = cards.drawPile.Count.ToString();
        discardDisplay.text = cards.discardPile.Count.ToString();
        manaDisplay.text = mana.ToString() + "/" + manaMax;
        if (enemy.enemyHP <= 0)
        {
            StartCoroutine(BaskInYourGlory());
        }
        if (player.playerHP <= 0)
        {
            SceneManager.LoadScene(4);
        }
    }

    private void Awake()
    {
        cards = GameObject.Find("PlayerCardHandler").GetComponent<PlayerCards>();
        enemy = enemyManager.GetComponent<EnemyManager>();
        player = playerManager.GetComponent<PlayerManager>();
        victoryScreen.enabled = false;
        victoryScreen.text = victoryTextNormal[Random.Range(1, victoryTextNormal.Count)];
        if (Random.Range(1,10) == 7)
        {
            victoryScreen.text = victoryTextDeranged[Random.Range(1, victoryTextDeranged.Count)];
        }
        cards.BeginCombat();
        PlayerTurn();
    }
    public void PlayerTurn()
    {
        if (cards == null)
        {
            cards = GameObject.Find("PlayerCardHandler").GetComponent<PlayerCards>();
        }
        manaMax++;
        mana = manaMax;
        cards.Draw(cards.cardsDrawnPerTurn);
        cards.OrganizeHand();
    }

    public void YouDecidedToEndYourTurnAndIWantToDoCoroutineStuffButIEnumeratorsAndIEnumerablesAreIncompatibleWithButtons()
    {
        StartCoroutine(EnemyTurn());
    }

    public IEnumerator EnemyTurn()
    {
        mana = 0;
        if (cards == null)
        {
            cards = GameObject.Find("PlayerCardHandler").GetComponent<PlayerCards>();
        }
        cards.DiscardHand();
        for (int i = 0; i < 8; i++)
        {
            yield return StartCoroutine(DoSummonStuff(i));
        }
        enemy.EnemyTurn();
        PlayerTurn();
    }

    private IEnumerator DoSummonStuff(int i)
    {
        SummonScript summon = summons[i].GetComponent<SummonScript>();
        if (summon.alive)
        {
            if (summon.canAttack)
            {
                summon.Attack();
                yield return new WaitForSeconds(0.5f);
            }
            else
            {
                summon.canAttack = true;
            }
        }
    }

    private IEnumerator BaskInYourGlory()
    {
        victoryScreen.enabled = true;
        hud.enabled = false;
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(1);
    }
}
