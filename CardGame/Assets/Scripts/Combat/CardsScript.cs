using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CardsScript : MonoBehaviour
{
    private Vector3 moveTo;
    public Vector3 restPosition = new Vector3(7,-4,0);
    private bool isFollowing;
    public CardTemplate card;
    public PlayerManager pManager;
    public EnemyManager eManager;
    public GameObject cardManager;
    private PlayerCards pCards;

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
        pCards = cardManager.GetComponent<PlayerCards>();
        Debug.Log(card);
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
        isFollowing = true;
    }
    void OnMouseUp()
    {
        isFollowing = false;
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (!isFollowing)
        {
            bool discard = true;
            card.target = collision.gameObject;
            switch (card.purpose)
            {
                case CardTemplate.EPurpose.Heal:
                    card.target.GetComponent<PlayerManager>().playerHP += card.attack;
                    break;
                case CardTemplate.EPurpose.Attack:
                    card.target.GetComponent<EnemyManager>().enemyHP -= card.attack;
                    break;
                case CardTemplate.EPurpose.Buff:
                    pManager.powerMod += card.attack;
                    break;
                case CardTemplate.EPurpose.Debuff:
                    card.target.GetComponent<EnemyManager>().actionIntensity -= card.attack;
                    break;
                default:
                    discard = false;
                    break;
            }
            if (discard)
            {
                Debug.Log(card);
                pCards.Discard(card);
                Destroy(gameObject);
            }
            Debug.Log(collision.gameObject);
        }
    }
}
