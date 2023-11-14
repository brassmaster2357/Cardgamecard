using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CardEvent : MonoBehaviour
{

    public CardTemplate[] cardArray;

    private CardTemplate randomCard;

    public GameObject pch;
    public PlayerCards pc;
    public GameObject eventObject;
    public EventLoader el;


    public GameObject cardBase;

    private int cardNum;

    void Start()
    {
        pch = GameObject.Find("PlayerCardHandler");

        eventObject = GameObject.Find("EventLoader");
        el = eventObject.GetComponent<EventLoader>();
        
        randomCard = cardArray[Random.Range(1, cardArray.Length)];
        cardNum = -1;
        LoadCard();

        randomCard = cardArray[Random.Range(1, cardArray.Length)];
        cardNum = 0;
        LoadCard();

        randomCard = cardArray[Random.Range(1, cardArray.Length)];
        cardNum = 1;
        LoadCard();
    }

    void Update()
    {
        
    }

    void LoadCard()
    {
        GameObject cardObject = Instantiate(cardBase, new Vector2(cardNum * 5, 0), Quaternion.identity);

        CardsScript tempScript = cardObject.GetComponent<CardsScript>();

        tempScript.card = randomCard;

        tempScript.LoadCard();

        tempScript.restPosition = cardObject.transform.position;
    }

    public void endCards(CardTemplate card)
    {
        pc.cardsTotal.Add(card);
        SceneManager.LoadScene(1);
        el.eventDecided = false;
    }
}
