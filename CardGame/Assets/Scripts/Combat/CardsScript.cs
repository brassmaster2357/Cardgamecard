using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CardsScript : MonoBehaviour
{
    private Vector3 moveTo;
    public Vector3 restPosition = new Vector3(0,-4,0);
    private bool isFollowing;
    public CardTemplate card;
    public GameObject cardManager;
    private PlayerCards pCards;
    public GameObject combatManager;
    private CombatManager combat;

    public GameObject eventLoader;
    private EventLoader events;
    private WizardEvent we;

    public BoxCollider2D cardCollider;

    public Image cardUI;
    public TextMeshProUGUI cardUIName;
    public TextMeshProUGUI cardUIDescription;
    public TextMeshProUGUI cardUICost;
    public Image cardUIArt;
    public Canvas cardSummonUI; // Attack/health stats for a card are stored separately from the rest of the card to make it easier to hide it on non-summon cards
    public TextMeshProUGUI cardSummonUIAttack;
    public TextMeshProUGUI cardSummonUIHealth;

    // Start is called before the first frame update
    public void LoadCard()
    {
        combatManager = GameObject.Find("CombatManager");
        eventLoader = GameObject.Find("EventLoader");

        pCards = cardManager.GetComponent<PlayerCards>();
        cardCollider = gameObject.GetComponent<BoxCollider2D>();
        combat = combatManager.GetComponent<CombatManager>();
        //restPosition.x = Random.Range(-7f, 7f);

        cardUIName.text = card.name;
        cardUIDescription.text = card.description;
        cardUICost.text = card.cost.ToString();
        cardUIArt.sprite = card.art;
        cardSummonUIAttack.text = card.attack.ToString();
        cardSummonUIHealth.text = card.health.ToString();
        if (card.purpose != CardTemplate.EPurpose.Summon)
        {
            cardSummonUI.enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isFollowing)
            moveTo = (gameObject.transform.position + Camera.main.ScreenToWorldPoint(Input.mousePosition)) / 2;
        else
            moveTo = (gameObject.transform.position + restPosition) / 2;
        moveTo.z = 0;
        gameObject.transform.position = moveTo;

        cardUI.transform.position = Camera.main.WorldToScreenPoint(moveTo);
    }

    void OnMouseDown()
    {
        if (eventLoader != null)
        {
            events = eventLoader.GetComponent<EventLoader>();
            we = eventLoader.GetComponent<WizardEvent>();
            EventLoader events = eventLoader.GetComponent<EventLoader>();
            if (events.nextEvent == "Wizard")
            {
                we.SelectedCard(card);
            }
        }
        else
        {
            if (combat.mana >= card.cost)
            {
                isFollowing = true;
                cardCollider.size /= 4;
            }
        }
    }
    void OnMouseUp()
    {
        if (eventLoader == null)
        {
            isFollowing = false;
            if (combat.mana >= card.cost)
            {
                cardCollider.size *= 4;
            }
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (!isFollowing)
        {
            bool discard = true;
            card.target = collision.gameObject;
            cardCollider.enabled = false;
            switch (card.purpose)
            {
                case CardTemplate.EPurpose.Heal: //Playing a heal card
                    switch (card.target.tag)
                    {
                        case "Player":
                            card.target.GetComponent<PlayerManager>().playerHP += card.health;
                            break;
                        case "SummonAlly":
                            card.target.GetComponent<SummonScript>().health += card.health;
                            break;
                        default:
                            discard = false;
                            break; //Don't use card if the target isn't friendly
                    }
                    break;
                case CardTemplate.EPurpose.Attack: //Playing an attack card
                    switch (card.target.tag)
                    {
                        case "Enemy":
                                card.target.GetComponent<EnemyManager>().enemyHP -= card.attack;
                                break;
                        case "SummonEnemy":
                                card.target.GetComponent<SummonScript>().health -= card.attack;
                            break;
                        default:
                            discard = false;
                            break; //Don't use card if the target isn't hostile
                    }
                    break;
                case CardTemplate.EPurpose.Buff: //Playing a buff card
                    switch (card.target.tag)
                    {
                        case "Player":
                            card.target.GetComponent<PlayerManager>().powerMod += card.attack;
                            break;
                        case "SummonAlly":
                            card.target.GetComponent<SummonScript>().attack += card.attack;
                            break;
                        default:
                            discard = false;
                            break; //Don't use card if the target isn't friendly
                    }
                    break;
                case CardTemplate.EPurpose.Debuff: //Playing a debuff card
                    switch (card.target.tag)
                    {
                        case "Enemy":
                            card.target.GetComponent<EnemyManager>().powerMod -= card.attack;
                            break;
                        case "SummonEnemy":
                            card.target.GetComponent<SummonScript>().attack -= card.attack;
                            break;
                        default:
                            discard = false;
                            break; //Don't use card if the target isn't hostile
                    }
                    break;
                case CardTemplate.EPurpose.Summon: //Summoning
                    if (card.target.tag == "EmptySummonAlly")
                    {
                        //Set the summon object's stats to the card played
                        SummonScript newSummon = card.target.GetComponent<SummonScript>(); 
                        newSummon.alive = true;
                        newSummon.attack = card.summon.attack;
                        newSummon.healthMax = card.summon.health;
                        newSummon.health = card.summon.health;
                        newSummon.canAttack = card.summon.canAttack;
                        newSummon.special = card.summon.special;
                    }
                    break;
                default:
                    discard = false;
                    break;
            }
            if (discard)
            {
                Debug.Log(card);
                combat.mana -= card.cost;
                pCards.Discard(card);
                Destroy(gameObject);
                pCards.OrganizeHand();
            }
            else { cardCollider.enabled = true; }
            Debug.Log(collision.gameObject);
        }
    }
}
