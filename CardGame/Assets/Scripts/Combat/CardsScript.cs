using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardsScript : MonoBehaviour
{
    private Vector3 moveTo;
    public Vector3 restPosition = new Vector3(0,-4,0);
    private bool isFollowing;
    public CardTemplate card;
    public PlayerManager pManager;
    public EnemyManager eManager;
    public GameObject cardManager;
    private PlayerCards cards;

    // Start is called before the first frame update
    void Awake()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = card.art;
        cards = cardManager.GetComponent<PlayerCards>();
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
    }

    void OnMouseDown()
    {
        isFollowing = true;
        Debug.Log("You have picked up the card");
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
                    card.target.GetComponent<PlayerManager>().playerHP += card.power;
                    break;
                case CardTemplate.EPurpose.Attack:
                    card.target.GetComponent<EnemyManager>().enemyHP -= card.power;
                    break;
                case CardTemplate.EPurpose.Buff:
                    pManager.powerMod += 2;
                    break;
                case CardTemplate.EPurpose.Debuff:
                    card.target.GetComponent<EnemyManager>().actionIntensity -= card.power;
                    break;
                default:
                    discard = false;
                    break;
            }
            if (discard)
            {
                cards.Discard(card, gameObject);
            }
            Debug.Log(collision.gameObject);
        }
    }
}
