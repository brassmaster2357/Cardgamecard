using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardEvent : MonoBehaviour
{

    public CardTemplate[] cardArray;

    private CardTemplate randomCard;

    public GameObject pch;
    public PlayerCards pc;


    public GameObject cardBase;

    private int cardNum;

    void Start()
    {
        pch = GameObject.Find("PlayerCardHandler");

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
}
