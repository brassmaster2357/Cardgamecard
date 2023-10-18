using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCards : MonoBehaviour
{
    private static PlayerCards instance;
    public List<CardTemplate> cardsTotal;
    public List<CardTemplate> drawPile;
    public List<CardTemplate> cardsInHand;
    public List<CardTemplate> discardPile;
    public GameObject card;
    public int cardHandPosition;

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        } else
        {
            DontDestroyOnLoad(transform.root);
            instance = this;
        }
    }

    public void BeginCombat()
    {
        drawPile.Clear();
        cardsInHand.Clear();
        drawPile = cardsTotal;
        cardHandPosition = 0;
        Draw(5);
    }

    public void Draw(int cardsToDraw)
    {
        for (int i = 0; i < cardsToDraw; i++)
        {
            if (drawPile.Count == 0)
            {
                //""""Shuffle"""" the discard pile back into the draw pile because we ran out of cards to draw
                drawPile = discardPile;
                discardPile.Clear();
                //Draw a card
                cardsInHand.Add(drawPile[0]);
                GameObject newCardDrawn = Instantiate(card, Vector3.down * 10, Quaternion.identity);
                CardsScript tempScript = newCardDrawn.GetComponent<CardsScript>();
                tempScript.card = drawPile[0];
                tempScript.enabled = true;
                tempScript.LoadCard();
                drawPile.RemoveAt(0);
                cardHandPosition++;
            }
            else
            {
                //Draw a card
                cardsInHand.Add(drawPile[0]);
                GameObject newCardDrawn = Instantiate(card, Vector3.down * 10, Quaternion.identity);
                CardsScript tempScript = newCardDrawn.GetComponent<CardsScript>();
                tempScript.card = drawPile[0];
                tempScript.enabled = true;
                tempScript.LoadCard();
                drawPile.RemoveAt(0);
                cardHandPosition++;
            }
        }
        Debug.Log(cardsInHand);
    }

    public void DiscardHand()
    {
        discardPile.AddRange(cardsInHand);
        cardsInHand.Clear();
        Debug.Log(discardPile);
        cardHandPosition = 0;
    }

    public void Discard(CardTemplate card)
    {
        discardPile.Add(card);
        cardsInHand.Remove(card);
        Debug.Log(discardPile);
        cardHandPosition--;
    }
}
