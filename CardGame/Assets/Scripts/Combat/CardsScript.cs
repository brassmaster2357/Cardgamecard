using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class CardsScript : MonoBehaviour
{
    private Vector3 moveTo;
    public Vector3 restPosition = new(0,-4,0);
    private bool isFollowing;
    public CardTemplate card;
    public GameObject cardManager;
    private PlayerCards pCards;
    public GameObject combatManager;
    private CombatManager combat;

    // Jake's stuff
    public GameObject eventLoader;
    private WizardEvent we;
    private CardEvent ce;
    private HavenEvent he;
    public int cardPos;

    public BoxCollider2D cardCollider;
    public float whyY; // BALDUR'S GATE 3 REFERENCE!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!

    // UI Handling
    public Image cardUI;
    public TextMeshProUGUI cardUIName;
    public TextMeshProUGUI cardUIDescription;
    public TextMeshProUGUI cardUICost;
    public Image cardUIArt;
    public Canvas cardSummonUI; // Attack/health stats for a card are stored separately from the rest of the card to make it easier to hide it on non-summon cards
    public TextMeshProUGUI cardSummonUIAttack;
    public TextMeshProUGUI cardSummonUIHealth;

    // Start is called before the first frame update
    
    void Awake()
    {
        cardManager = GameObject.FindGameObjectWithTag("PCH");
        pCards = cardManager.GetComponent<PlayerCards>();
        TimedThings();
    }
    
    public IEnumerator TimedThings()
    {
        while (true)
        {
            if (!isFollowing)
                cardCollider.enabled = true;
            yield return new WaitForSeconds(0.25f);
        }
    }
    
    public void LoadCard()
    {
        if (combatManager == null)
            combatManager = GameObject.Find("CombatManager");
        if (eventLoader == null)
            eventLoader = GameObject.Find("EventLoader");
        if (combatManager != null)
        {
            combat = combatManager.GetComponent<CombatManager>();
        }
        pCards = cardManager.GetComponent<PlayerCards>();
        cardCollider = gameObject.GetComponent<BoxCollider2D>();
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
    void FixedUpdate()
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
        Debug.Log("Click");
        GameObject ec = GameObject.FindGameObjectWithTag("EC");
        GameObject el = GameObject.Find("EventLoader");
        if (combatManager != null)
        {
            if (combat.mana >= card.cost)
            {
                isFollowing = true;
                cardCollider.size /= 4;
            }
        }
        
        we = ec.GetComponent<WizardEvent>();
        ce = ec.GetComponent<CardEvent>();
        he = ec.GetComponent<HavenEvent>();

        if (SceneManager.GetActiveScene().name == "Wizard")
        {
            we.arrayPos = cardPos;
            we.SelectedCardW(card);
        }
        else if (SceneManager.GetActiveScene().name == "Cards")
        {
            ce.endCards(card);
        }
        else if (SceneManager.GetActiveScene().name == "Haven")
        {
            he.arrayPos = cardPos;
            he.SelectedCardH(card);
        }

    }
    void OnMouseUp()
    {
        whyY = transform.position.y;
        if (combatManager != null)
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
        if (!Input.GetMouseButton(0))
            cardCollider.enabled = false;
        card.target = collision.gameObject;
        if (!isFollowing && card.target == collision.gameObject && combat.mana >= card.cost && (collision.gameObject.transform.position.y - whyY) <= 0.5)
        {
            bool discard = true;
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
                            if (card.name == "More Cards") { pCards.Draw(3); }
                            break;
                        case "SummonAlly":
                            card.target.GetComponent<SummonScript>().attack += card.attack;
                            if (card.name == "More Cards") { pCards.Draw(3); }
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
                    if (card.target.CompareTag("EmptySummonAlly"))
                    {
                        //Set the summon object's stats to the card played
                        SummonScript newSummon = card.target.GetComponent<SummonScript>(); 
                        newSummon.alive = true;
                        newSummon.attack = card.summon.attack;
                        newSummon.healthMax = card.summon.health;
                        newSummon.health = card.summon.health;
                        newSummon.canAttack = card.summon.canAttack;
                        newSummon.special = card.summon.special;
                        newSummon.sprenderer.sprite = card.summon.art;
                    }
                    break;
                default:
                    discard = false;
                    break;
            }
            if (discard && combat.mana >= card.cost)
            {
                Debug.Log(card);
                combat.mana -= card.cost;
                pCards.Discard(card);
                Destroy(gameObject);
                pCards.OrganizeHand();
                Debug.Log(pCards.discardPile);
            }
            else { cardCollider.enabled = true; card.target = null; }
            Debug.Log(collision.gameObject);
            cardCollider.enabled = true;
        }
    }
}
