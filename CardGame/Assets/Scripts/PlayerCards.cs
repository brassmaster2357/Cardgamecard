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
        discardPile = cardsTotal;
        Draw(3);
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
                cardsToDraw++; // Make it loop one more time instead of duplicating card draw code
            }
            else
            {
                //Draw a card
                cardsInHand.Add(drawPile[0]);
                GameObject newCardDrawn = Instantiate(card, Vector3.down * 10, Quaternion.identity);
                newCardDrawn.GetComponent<CardsScript>().card = drawPile[0];
                drawPile.RemoveAt(0);
            }
        }
        Debug.Log(cardsInHand);
    }

    public void DiscardHand()
    {
        discardPile.AddRange(cardsInHand);
        cardsInHand.Clear();
        Debug.Log(discardPile);
    }

    public void Discard(CardTemplate card, GameObject cardUIObject)
    {
        discardPile.Add(card);
        cardsInHand.Remove(card);
        Destroy(cardUIObject);
        Debug.Log(discardPile);
    }
}
